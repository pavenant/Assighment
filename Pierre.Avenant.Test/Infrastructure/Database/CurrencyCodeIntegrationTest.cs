using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pierre.Avenant.Assignment.Infrastructure.Database;

namespace Pierre.Avenant.Assignment.Test.Infrastructure.Database
{
    [TestClass]
    public class CurrencyCodeIntegrationTest
    {
        //todo cache this results
        [TestMethod]
        public void GetCurrencyCodes_Success()
        {
            var a = new CurrencyCodeRepository();
            var result = a.GetCurrencyCodes();
            Assert.IsTrue(result.Any());
        }
    }
}
