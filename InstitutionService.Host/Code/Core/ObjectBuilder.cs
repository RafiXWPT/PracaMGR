using System;
using Domain;
using Domain.Interfaces;
using Domain.Inventory;
using Domain.Residence;
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

            Container.Register<IDbRepository>(() => new InstitutionServiceDatabaseContext("InstitutionContext"));
            Container.Register<IRepository<Patient>, DatabasePatientRepository>(Lifestyle.Transient);
            Container.Register<IRepository<Hospitalization>, DatabaseHospitalizationRepository>(Lifestyle.Transient);
            Container.Register<IRepository<Treatment>, DatabaseTreatmentRepository>(Lifestyle.Transient);
            Container.Register<IRepository<Examination>, DatabaseExaminationRepository>(Lifestyle.Transient);
            Container.Register<IRepository<UsedMedicine>, DatabaseUsedMedicineRepository>(Lifestyle.Transient);
            Container.Register<IRepository<Medicine>, DatabaseMedicineRepository>(Lifestyle.Transient);

            Container.Verify();

            _initialized = true;
        }
    }
}