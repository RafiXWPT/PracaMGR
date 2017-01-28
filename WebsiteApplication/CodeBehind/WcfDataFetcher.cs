using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Domain;
using Domain.Interfaces;
using InstitutionService;

namespace WebsiteApplication.CodeBehind
{
    internal class WcfDataFetcher
    {
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
                //(client as ICommunicationObject)?.Abort();
                return null;
            }

            return client;
        }

        private void CloseConnection(IInstitutionService client)
        {
            (client as ICommunicationObject)?.Close();
        }

        private readonly IInstitutionRepository _repository;

        public WcfDataFetcher(IInstitutionRepository repository)
        {
            _repository = repository;
        }

        public PatientTransferObject GetPatientBasicInfo(string pesel, Institution institution)
        {
            return GetPatientHistory(pesel, institution, false);
        }

        public PatientTransferObject GetPatientFullInfo(string pesel, Institution institution)
        {
            return GetPatientHistory(pesel, institution, true);
        }

        public PatientTransferObject GetPatientHistory(string pesel)
        {
            if (!_repository.Institutions.Any())
                return null;

            var patientRecords = new List<PatientTransferObject>();
            foreach (var institution in _repository.Institutions)
            {
                patientRecords.Add(GetPatientFullInfo(pesel, institution));
            }

            var outputPatientData = new PatientTransferObject();

            foreach (var outputHospitalization in patientRecords)
            {
                outputPatientData.Hospitalizations.AddRange(outputHospitalization.Hospitalizations);
            }

            return outputPatientData;
        }

        private PatientTransferObject GetPatientHistory(string pesel, Institution institution, bool fullHistory)
        {
            var connection = EstablishConnection(institution.InstitutionEndpointAddress);
            var patient = fullHistory ? connection.GetPatientBasicInfo(pesel) : connection.GetPatientFullInfo(pesel);
            CloseConnection(connection);
            return patient;
        }
    }
}
