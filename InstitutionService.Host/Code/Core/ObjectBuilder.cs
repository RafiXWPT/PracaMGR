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
        private static bool _initialized;
        private static readonly Lazy<Container> Lazy = new Lazy<Container>(() => new Container());
        public static Container Container => Lazy.Value;

        public static void Initialize()
        {
            if (_initialized)
                return;

            Container.Options.DefaultScopedLifestyle = new WcfOperationLifestyle();

            Container.Register<IRepository>(() => new InstitutionServiceDatabaseContext("InstitutionContext"));
            Container.Register<IPatientRepository, DatabasePatientRepository>();
            Container.Register<IHospitalizationRepository, DatabaseHospitalizationRepository>();
            Container.Register<ITreatmentRepository, DatabaseTreatmentRepository>();
            Container.Register<IExaminationRepository, DatabaseExaminationRepository>();
            Container.Register<IUsedMedicineRepository, DatabaseUsedMedicineRepository>();
            Container.Register<IMedicineRepository, DatabaseMedicineRepository>();

            Container.Verify();

            _initialized = true;
        }

        public static void CleanUp()
        {
            Container.Dispose();
        }
    }
}
