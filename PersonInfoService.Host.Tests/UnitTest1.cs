using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonInfoService.Host.Code.DataAccessLayer;

namespace PersonInfoService.Host.Tests
{
    [TestClass]
    public class PersonInfoService
    {
        [TestMethod]
        public void GetDatabase()
        {
            var repository = new PersonInfoServiceDatabaseContext();
            Assert.IsNotNull(repository);
        }
    }
}