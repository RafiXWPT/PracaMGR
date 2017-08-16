using Domain;
using Domain.Interfaces;
using Domain.Inventory;
using Domain.Residence;
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
            var repository = ObjectBuilder.Container.GetInstance<IRepository<Patient>>();
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void GetHospitalizationRepository()
        {
            var repository = ObjectBuilder.Container.GetInstance<IRepository<Hospitalization>>();
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void GetExaminationRepository()
        {
            var repository = ObjectBuilder.Container.GetInstance<IRepository<Examination>>();
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void GetTreatmentRepository()
        {
            var repository = ObjectBuilder.Container.GetInstance<IRepository<Treatment>>();
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void GetMedicineRepository()
        {
            var repository = ObjectBuilder.Container.GetInstance<IRepository<Medicine>>();
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void GetUsedMedicineRepository()
        {
            var repository = ObjectBuilder.Container.GetInstance<IRepository<UsedMedicine>>();
            Assert.IsNotNull(repository);
        }
    }
}