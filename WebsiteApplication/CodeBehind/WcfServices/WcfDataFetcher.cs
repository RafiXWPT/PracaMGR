using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using Domain.Interfaces;
using Domain.Residence;
using InstitutionService;
using WebsiteApplication.Models.ViewModels.Tile;

namespace WebsiteApplication.CodeBehind.WcfServices
{
    internal class WcfDataFetcher : IDisposable
    {
        private readonly IRepository<Institution> _repository;
        private readonly IDateTimeCountableRepository<SearchHistory> _searchHistoryRepository;
        private readonly string _username;
        private IInstitutionService _connection;

        public WcfDataFetcher(IRepository<Institution> repository, IDateTimeCountableRepository<SearchHistory> searchHistoryRepository, string username)
        {
            _repository = repository;
            _searchHistoryRepository = searchHistoryRepository;
            _username = username;
        }

        public void Dispose()
        {
            CloseConnection(_connection);
        }

        private static IInstitutionService EstablishConnection(string urlEndpoint)
        {
            var binding = new NetTcpBinding();
            binding.MaxBufferSize = int.MaxValue;
            binding.MaxReceivedMessageSize = int.MaxValue;
            var channel = new ChannelFactory<IInstitutionService>(binding, urlEndpoint);

            IInstitutionService client;
            try
            {
                client = channel.CreateChannel();
                client.Ping();
            }
            catch (EndpointNotFoundException)
            {
                return null;
            }

            return client;
        }

        private void CloseConnection(IInstitutionService client)
        {
            (client as ICommunicationObject)?.Close();
        }

        public List<TViewModel> GetAllPatientsFromInstitution<TViewModel>(Guid institutionId) where TViewModel: class
        {
            var institution = _repository.Read(institutionId);
            if (institution != null)
                _connection = EstablishConnection(institution.InstitutionEndpointAddress);

            if(_connection == null)
                return new List<TViewModel>();

            var allPatientsFromInstitution = _connection?.GetAllPatients();
            _searchHistoryRepository.Create(new SearchHistory
            {
                CreatedAt = DateTime.Now,
                CreatedBy = _username,
                SearchType = SearchType.ALL_PATIENTS_FROM_INSTITUTION
            });

            return Mapper.Map<List<TViewModel>>(allPatientsFromInstitution);
        }

        public TViewModel GetPatient<TViewModel>(string pesel, bool fullHistory) where TViewModel : class
        {
            if (!_repository.Entities.Any())
                return null;

            var patientRecords = new List<PatientTransferObject>();
            Parallel.ForEach(_repository.Entities, institution =>
            {
                var patient = GetPatient(pesel, institution, fullHistory);
                if (patient != null)
                    patientRecords.Add(patient);
            });

            _searchHistoryRepository.Create(new SearchHistory
            {
                CreatedAt = DateTime.Now,
                CreatedBy = _username,
                PatientPesel = pesel,
                SearchType = fullHistory ? SearchType.FULL_HISTORY : SearchType.BASIC_HISTORY
            });

            return Mapper.Map<TViewModel>(new PatientTransferObject
            {
                Pesel = pesel,
                Hospitalizations = patientRecords.SelectMany(pr => pr.Hospitalizations).ToList()
            });
        }

        public TViewModel GetHospitalization<TViewModel>(Guid hospitalizationId, string endpoint) where TViewModel: class
        {
            _connection = EstablishConnection(endpoint);

            var hospitalization = _connection?.GetHospitalization(hospitalizationId);

            _searchHistoryRepository.Create(new SearchHistory
            {
                CreatedAt = DateTime.Now,
                CreatedBy = _username,
                HospitalizationId = hospitalizationId,
                SearchType = SearchType.HOSPITALIZATION
            });

            return Mapper.Map<TViewModel>(hospitalization);
        }

        public TViewModel GetExamination<TViewModel>(Guid examinationId, string endpoint) where TViewModel: class
        {
            _connection = EstablishConnection(endpoint);

            var examination = _connection?.GetExamination(examinationId);

            _searchHistoryRepository.Create(new SearchHistory
            {
                CreatedAt = DateTime.Now,
                CreatedBy = _username,
                ExaminationId = examinationId,
                SearchType = SearchType.EXAMINATION
            });

            return Mapper.Map<TViewModel>(examination);
        }

        public TViewModel GetTreatment<TViewModel>(Guid treatmentId, string endpoint) where TViewModel: class
        {
            _connection = EstablishConnection(endpoint);

            var treatment = _connection?.GetTreatment(treatmentId);

            _searchHistoryRepository.Create(new SearchHistory
            {
                CreatedAt = DateTime.Now,
                CreatedBy = _username,
                TreatmentId = treatmentId,
                SearchType = SearchType.TREATMENT
            });

            return Mapper.Map<TViewModel>(treatment);
        }

        public TViewModel GetDocument<TViewModel>(Guid hospitalizationDocumentId, string endpoint) where TViewModel : class
        {
            _connection = EstablishConnection(endpoint);

            var document = _connection?.GetDocument(hospitalizationDocumentId);

            return Mapper.Map<TViewModel>(document);
        }

        private PatientTransferObject GetPatient(string pesel, Institution institution, bool fullHistory)
        {
            _connection = EstablishConnection(institution.InstitutionEndpointAddress);
            var patient = _connection?.GetPatient(pesel, fullHistory);
            if (patient == null)
                return null;

            foreach (var h in patient.Hospitalizations)
                h.InstitutionId = institution.InstitutionId;

            return patient;
        }
    }
}