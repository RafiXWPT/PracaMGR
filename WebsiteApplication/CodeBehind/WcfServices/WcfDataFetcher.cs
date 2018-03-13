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
        private readonly IRepository<SearchHistory> _searchHistoryRepository;
        private readonly string _username;
        private IInstitutionService _connection;

        public WcfDataFetcher(IRepository<Institution> repository, IRepository<SearchHistory> searchHistoryRepository, string username)
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

        public TViewModel GetPatientHistory<TViewModel>(string pesel) where TViewModel: class
        {
            if (!_repository.Entities.Any())
                return null;

            var patientRecords = new List<PatientHistoryTransferObject>();
            Parallel.ForEach(_repository.Entities, institution =>
            {
                var patientHistory = GetPatientHistory<PatientHistoryTransferObject>(pesel, institution);
                if (patientHistory != null)
                    patientRecords.Add(patientHistory);
            });

            var patientFullHistory = new PatientHistoryTransferObject
            {
                Pesel = pesel
            };

            foreach (var patientRecord in patientRecords)
            foreach (var hospitalization in patientRecord.Hospitalizations)
                patientFullHistory.Hospitalizations.Add(hospitalization);

            _searchHistoryRepository.Create(new SearchHistory
            {
                CreatedAt = DateTime.Now,
                CreatedBy = _username,
                PatientPesel = pesel,
                SearchType = SearchType.FULL_HISTORY
            });

            return Mapper.Map<TViewModel>(patientFullHistory);
        }

        private TViewModel GetPatientHistory<TViewModel>(string pesel, Institution institution) where TViewModel: class
        {
            _connection = EstablishConnection(institution.InstitutionEndpointAddress);
            var history = _connection?.GetPatientFullHistory(pesel);
            if (history == null)
                return null;

            foreach (var h in history.Hospitalizations)
                h.HospitalizationId = institution.InstitutionId;

            return Mapper.Map<TViewModel>(history);
        }

        public List<TViewModel> GetPatientInfo<TViewModel>(string pesel) where TViewModel: class
        {
            if (!_repository.Entities.Any())
                return null;

            var patientRecords = new List<TViewModel>();
            Parallel.ForEach(_repository.Entities, institution =>
            {
                var patientInfo = GetPatientInfo<TViewModel>(pesel, institution);
                if (patientInfo != null)
                    patientRecords.Add(patientInfo);
            });

            _searchHistoryRepository.Create(new SearchHistory
            {
                CreatedAt = DateTime.Now,
                CreatedBy = _username,
                PatientPesel = pesel,
                SearchType = SearchType.BASIC_HISTORY
            });

            return patientRecords;
        }

        private TViewModel GetPatientInfo<TViewModel>(string pesel, Institution institution) where TViewModel: class
        {
            _connection = EstablishConnection(institution.InstitutionEndpointAddress);

            if (_connection == null)
                return null;

            var patient = _connection.GetPatientInfo(pesel);
            if (patient != null)
                patient.InstitutionId = institution.InstitutionId;

            return Mapper.Map<TViewModel>(patient);
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
    }
}