using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Domain;
using Domain.Interfaces;
using Domain.Residence;
using InstitutionService;

namespace WebsiteApplication.CodeBehind
{
    internal class WcfDataFetcher : IDisposable
    {
        private readonly IInstitutionRepository _repository;
        private IInstitutionService Connection;

        public WcfDataFetcher(IInstitutionRepository repository)
        {
            _repository = repository;
        }

        public void Dispose()
        {
            CloseConnection(Connection);
        }

        private IInstitutionService EstablishConnection(string urlEndpoint)
        {
            var binding = new NetTcpBinding();
            var channel = new ChannelFactory<IInstitutionService>(binding, urlEndpoint);

            IInstitutionService client;
            try
            {
                client = channel.CreateChannel();
            }
            catch (Exception)
            {
                return null;
            }

            return client;
        }

        private void CloseConnection(IInstitutionService client)
        {
            (client as ICommunicationObject)?.Close();
        }

        public List<PatientTransferObject> GetPatientHistory(string pesel)
        {
            if (!_repository.Institutions.Any())
                return null;

            var patientRecords = new List<PatientTransferObject>();
            Parallel.ForEach(_repository.Institutions, institution =>
            {
                var patientHistory = GetPatientHistory(pesel, institution);
                if (patientHistory != null)
                    patientRecords.Add(patientHistory);
            });

            return patientRecords;
        }

        private PatientTransferObject GetPatientHistory(string pesel, Institution institution)
        {
            Connection = EstablishConnection(institution.InstitutionEndpointAddress);

            if (Connection == null)
                return null;

            var patient = Connection.GetPatientInfo(pesel);
            if (patient != null)
                patient.InstitutionId = institution.InstitutionId;

            return patient;
        }

        public HospitalizationTransferObject GetHospitalization(Guid hospitalizationId, string endpoint)
        {
            Connection = EstablishConnection(endpoint);

            if (Connection == null)
                return null;

            var hospitalization = Connection.GetHospitalization(hospitalizationId);

            return hospitalization;
        }

        public ExaminationTransferObject GetExamination(Guid examinationId, string endpoint)
        {
            Connection = EstablishConnection(endpoint);

            if (Connection == null)
                return null;

            var examination = Connection.GetExamination(examinationId);

            return examination;
        }

        public TreatmentTransferObject GetTreatment(Guid treatmentId, string endpoint)
        {
            Connection = EstablishConnection(endpoint);

            if (Connection == null)
                return null;

            var treatment = Connection.GetTreatment(treatmentId);

            return treatment;
        }
    }
}