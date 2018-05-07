using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pierre.Avenant.Assignment.Infrastructure.Database;

namespace Pierre.Avenant.Assignment.Test.IntegrationTests.Infrastructure.Database
{
    [TestClass]
    public class CurrencyCodeIntegrationTest
    {

        //todo cache this results
        [TestMethod]
        public void GetCurrencyCodes_Success()
        {
            var a = new CurrencyCodeRepository(Configuration.ConnectionString);
            var result = a.GetCurrencyCodes();
            Assert.IsTrue(result.Any());
        }
    }
}
