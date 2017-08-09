using System;
using System.Configuration;
using System.ServiceModel;
using Domain;
using InstitutionService;

namespace WebsiteApplication.CodeBehind.WcfServices
{
    internal class WcfPersonInfoFetcher
    {
        private readonly string _personDatabaseEndpoint = ConfigurationManager.AppSettings["PERSON_DATABASE_ENDPOINT"];

        private IPersonInfoService EstablishConnection()
        {
            var binding = new NetTcpBinding();
            var channel = new ChannelFactory<IPersonInfoService>(binding, _personDatabaseEndpoint);
            IPersonInfoService client;
            try
            {
                client = channel.CreateChannel();
                if (!client.Ping())
                    throw new Exception("Host zdalny zwrócił błędną odpowiedź powitalną.");
            }
            catch (Exception)
            {
                return null;
            }

            return client;
        }

        private void CloseConnection(IPersonInfoService client)
        {
            (client as ICommunicationObject)?.Close();
        }

        public PersonTransferObject GetPersonInfo(string pesel)
        {
            var connection = EstablishConnection();
            if (connection == null)
                return null;

            var person = connection.GetPersonInfo(pesel);
            CloseConnection(connection);
            return person;
        }
    }
}