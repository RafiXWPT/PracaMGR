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
            Container.Register<IRepository<Patient>, PatientRepository>(Lifestyle.Transient);
            Container.Register<IRepository<HospitalizationDocument>, HospitalizationDocumentRepository>(Lifestyle.Transient);
            Container.Register<IRepository<Hospitalization>, HospitalizationRepository>(Lifestyle.Transient);
            Container.Register<IRepository<Treatment>, TreatmentRepository>(Lifestyle.Transient);
            Container.Register<IRepository<Examination>, ExaminationRepository>(Lifestyle.Transient);
            Container.Register<IRepository<UsedMedicine>, UsedMedicineRepository>(Lifestyle.Transient);
            Container.Register<IRepository<Medicine>, MedicineRepository>(Lifestyle.Transient);

            Container.Verify();

            _initialized = true;
        }
    }
}