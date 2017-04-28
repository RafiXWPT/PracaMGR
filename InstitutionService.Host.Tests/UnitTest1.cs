using Domain.Interfaces;
using InstitutionService.Host.Code.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InstitutionService.Host.Tests
{
    [TestClass]
    public class RepositoryGetter
    {
        [TestMethod]
        public void InitializeObjectBuilder()
        {
            ObjectBuilder.Initialize();
        }

        [TestMethod]
        public void GetPatientRepository()
        {
            var repository = ObjectBuilder.Container.GetInstance<IPatientRepository>();
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void GetHospitalizationRepository()
        {
            var repository = ObjectBuilder.Container.GetInstance<IHospitalizationRepository>();
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void GetExaminationRepository()
        {
            var repository = ObjectBuilder.Container.GetInstance<IExaminationRepository>();
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void GetTreatmentRepository()
        {
            var repository = ObjectBuilder.Container.GetInstance<ITreatmentRepository>();
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void GetMedicineRepository()
        {
            var repository = ObjectBuilder.Container.GetInstance<IMedicineRepository>();
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void GetUsedMedicineRepository()
        {
            var repository = ObjectBuilder.Container.GetInstance<IUsedMedicineRepository>();
            Assert.IsNotNull(repository);
        }
    }
}