using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using InstitutionService.Host.Code.DataAccessLayer;
using InstitutionService.Host.Code.DatabaseProvider;
using SimpleInjector;
using SimpleInjector.Integration.Wcf;

namespace InstitutionService.Host.Code.Core
{
    public static class SimpleInjector
    {
        private static readonly Lazy<Container> Lazy = new Lazy<Container>(() => new Container());
        public static Container Instance => Lazy.Value;

        public static void Initialize()
        {
            Instance.Options.DefaultScopedLifestyle = new WcfOperationLifestyle();
            Instance.Register<IPatientRepository, DatabaseRepository>();
            Instance.Register<IDatabaseContext>(() => new InstitutionServiceDatabaseContext());
            Instance.Verify();
        }
    }
}
