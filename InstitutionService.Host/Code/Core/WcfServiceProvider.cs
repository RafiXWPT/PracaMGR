using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using InstitutionService.Host.Code.DataAccessLayer;

namespace InstitutionService.Host.Code.Core
{
    class WcfServiceProvider
    {
        public void RunService()
        {

            using (var host = new ServiceHost(typeof(InstitutionService)))
            {
                host.Open();
                Console.WriteLine("Service Host Started");
                Console.ReadLine();
            }
        }
    }
}
