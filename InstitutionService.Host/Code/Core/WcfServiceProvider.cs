using System;
using System.ServiceModel;

namespace InstitutionService.Host.Code.Core
{
    internal class WcfServiceProvider
    {
        public void RunService()
        {
            using (var host = new ServiceHost(typeof(InstitutionService)))
            {
                host.Open();
                Console.WriteLine($"Service Host Started");
                Console.ReadLine();
            }
        }
    }
}