﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pierre.Avenant.Assignment.Core.Interfaces;
using Pierre.Avenant.Assignment.Core.Interfaces.Database;
using Pierre.Avenant.Assignment.Core.Interfaces.Services;
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

            ICellValidator cellValidator = new CurrencyCodeCellValidator(currencyMock.Object);
            var result = cellValidator.Validate("GBP");
            Assert.AreEqual(true,result);

        }

        [TestMethod]
        public void Validate_InvalidTaxCodeSuccess()
        {
            var currencyMock = new Mock<ICurrencyCodeRepository>();
            currencyMock.Setup(x => x.GetCurrencyCodes())
                .Returns(new Dictionary<string, string>() { { "GBP", "British Pound" } });

            ICellValidator cellValidator = new CurrencyCodeCellValidator(currencyMock.Object);
            var result = cellValidator.Validate("ZZZ");
            Assert.AreEqual(false, result);
        }
    }
}
