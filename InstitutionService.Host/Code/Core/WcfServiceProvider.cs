using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InstitutionService.Host.Code.Core
{
    class WcfServiceProvider
    {
        public void RunService()
        {
            using (ServiceHost host = new ServiceHost(typeof(InstitutionService)))
            {
                host.Open();
                Console.WriteLine("Service Host Started");
                Console.ReadLine();
            }
        }
    }
}
