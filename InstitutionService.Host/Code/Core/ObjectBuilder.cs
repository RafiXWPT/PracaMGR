using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using InstitutionService.Host.Code.DataAccessLayer;
using InstitutionService.Host.Code.DatabaseProvider;
using SimpleInjector;
using SimpleInjector.Integration.Wcf;
using SimpleInjector.Integration.Web;

namespace InstitutionService.Host.Code.Core
{
    public static class ObjectBuilder
    {
        private static readonly Lazy<Container> Lazy = new Lazy<Container>(() => new Container());
        public static Container Container => Lazy.Value;

        public static void Initialize()
        {
            Container.Options.DefaultScopedLifestyle = new WcfOperationLifestyle();

            Container.Register<IPatientRepository, DatabaseRepository>();
            Container.Register<IDatabaseContext>(() => new InstitutionServiceDatabaseContext("InstitutionContext"));
            Container.Verify();
        }
    }
}
