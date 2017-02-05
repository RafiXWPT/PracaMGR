using System;
using System.ServiceModel;

namespace PersonInfoService.Host.Code.Core
{
    class WcfServiceProvider
    {
        public void RunService()
        {

            using (var host = new ServiceHost(typeof(PersonInfoService)))
            {
                host.Open();
                Console.WriteLine("Service Host Started");
                Console.ReadLine();
            }
        }
    }
}
