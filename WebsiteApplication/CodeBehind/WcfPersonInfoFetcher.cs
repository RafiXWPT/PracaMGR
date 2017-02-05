using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Domain;
using InstitutionService;

namespace WebsiteApplication.CodeBehind
{
    class WcfPersonInfoFetcher
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
            var person = connection.GetPersonInfo(pesel);
            CloseConnection(connection);
            return person;
        }
    }
}
