using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using InstitutionService;

namespace DumyConsoleApplication
{
    class Program
    {

        static List<EndpointAddress> GetEndPoints()
        {
            return new List<EndpointAddress>()
            {
                new EndpointAddress("net.tcp://localhost:11070/InstitutionService"),
                //new EndpointAddress("net.tcp://localhost:11170/InstitutionService"),
                //new EndpointAddress("net.tcp://localhost:11270/InstitutionService")
            };
        }

        static void GetInstitutionName(IInstitutionService client)
        {
            Console.WriteLine(client.GetInstitutionName());
        }

        static void GetPatientInfo(IInstitutionService client)
        {
            var patient = client.GetPatientInfo("26112804811");
        }

        static void Main(string[] args)
        {

            foreach (var endpoint in GetEndPoints())
            {
                var binding = new NetTcpBinding();
                var channel = new ChannelFactory<IInstitutionService>(binding, endpoint);
                IInstitutionService client = null;

                try
                {
                    client = channel.CreateChannel();
                    GetInstitutionName(client);
                    GetPatientInfo(client);

                    (client as ICommunicationObject).Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    if (client != null)
                    {
                        (client as ICommunicationObject).Abort();
                    }
                }
            }

            Console.ReadLine();

        }
    }
}
