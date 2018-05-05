using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pierre.Avenant.Assignment.Core.Interfaces;
using Pierre.Avenant.Assignment.Core.Interfaces.Services;
using Pierre.Avenant.Assignment.Core.Services.Validation;

namespace Pierre.Avenant.Assignment.Test.Services.Validation
{
    [TestClass]
    public class AmountValidatorTest
    {
        [TestMethod]
        public void Validate_Success()
        {
            IValidator validator = new AmountValidator();
            var result = validator.Validate("1.100");
            Assert.AreEqual(true,result);
        }

        [TestMethod]
        public void Validate_NotValidDecimal_Failure()
        {
            IValidator validator = new AmountValidator();
            var result = validator.Validate("notvalid");
            Assert.AreEqual(false, result);
        }
    }
}
