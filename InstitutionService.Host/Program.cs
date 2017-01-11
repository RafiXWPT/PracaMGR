using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using InstitutionService.Host.Code.Core;
using InstitutionService.Host.Code.DummyDatabaseInitializer;
using DependencyContainer = InstitutionService.Host.Code.Core.SimpleInjector;

namespace InstitutionService.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            DependencyContainer.Initialize();
            var init = new InstitutionDatabaseInitializer();
            init.Seed(DependencyContainer.Instance.GetInstance<IDatabaseContext>());
            var wcfProvider = new WcfServiceProvider();
            wcfProvider.RunService();
        }
    }
}
