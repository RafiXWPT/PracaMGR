using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
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
            var connection = EstablishConnection(institution.InstitutionEndpointAddress);

            if(connection == null)
                return null;

            var patient = connection.GetPatientInfo(pesel);
            CloseConnection(connection);
            return patient;
        }
    }
}
