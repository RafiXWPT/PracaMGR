using System;
using Domain.Interfaces;
using InstitutionService.Host.Code.DataAccessLayer;
using InstitutionService.Host.Code.DatabaseProvider;
using SimpleInjector;
using SimpleInjector.Integration.Wcf;

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
            Container.Register<IPatientRepository, DatabasePatientRepository>(Lifestyle.Transient);
            Container.Register<IHospitalizationRepository, DatabaseHospitalizationRepository>(Lifestyle.Transient);
            Container.Register<ITreatmentRepository, DatabaseTreatmentRepository>(Lifestyle.Transient);
            Container.Register<IExaminationRepository, DatabaseExaminationRepository>(Lifestyle.Transient);
            Container.Register<IUsedMedicineRepository, DatabaseUsedMedicineRepository>(Lifestyle.Transient);
            Container.Register<IMedicineRepository, DatabaseMedicineRepository>(Lifestyle.Transient);

            Container.Verify();

            _initialized = true;
        }
    }
}