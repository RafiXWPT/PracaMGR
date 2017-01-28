using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
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
                //(client as ICommunicationObject)?.Abort();
                return null;
            }

            return client;
        }

        private readonly IInstitutionRepository _repository;

        public WcfDataFetcher(IInstitutionRepository repository)
        {
            _repository = repository;
        }

        private void CloseConnection(IInstitutionService client)
        {
            (client as ICommunicationObject)?.Close();
        }

        public PatientTransferObject GetPatient(string pesel, bool basicInfo = true)
        {
            if (!_repository.Institutions.Any())
                return null;

            var connection = EstablishConnection(_repository.Institutions.FirstOrDefault()?.InstitutionEndpointAddress);

            var patient = basicInfo ? connection.GetPatientBasicInfo(pesel) : connection.GetPatientFullInfo(pesel);

            CloseConnection(connection);

            return patient;
        }
    }
}
