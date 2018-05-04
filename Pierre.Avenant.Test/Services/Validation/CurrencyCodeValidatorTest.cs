using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pierre.Avenant.Assignment.Core.Interfaces;
using Pierre.Avenant.Assignment.Core.Services.Validation;

namespace Pierre.Avenant.Assignment.Test.Services.Validation
{
    [TestClass]
    public class CurrencyCodeValidatorTest
    {
        [TestMethod]
        public void Validate_ValidTaxCodeSuccess()
        {
            var currencyMock = new Mock<ICurrencyCodeRepository>();
            currencyMock.Setup(x => x.GetCurrencyCodes())
                .Returns(new Dictionary<string, string>() { {"GBP","British Pound"}});

            IValidator validator = new CurrencyCodeValidator(currencyMock.Object);
            var result = validator.Validate("GBP");
            Assert.AreEqual(true,result);

        }

        [TestMethod]
        public void Validate_InvalidTaxCodeSuccess()
        {
            var currencyMock = new Mock<ICurrencyCodeRepository>();
            currencyMock.Setup(x => x.GetCurrencyCodes())
                .Returns(new Dictionary<string, string>() { { "GBP", "British Pound" } });

            IValidator validator = new CurrencyCodeValidator(currencyMock.Object);
            var result = validator.Validate("ZZZ");
            Assert.AreEqual(false, result);
        }
    }
}
