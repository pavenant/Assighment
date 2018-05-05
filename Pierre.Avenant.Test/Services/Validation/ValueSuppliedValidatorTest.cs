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
    public class ValueSuppliedValidatorTest
    {
        [TestMethod]
        public void Validate_ValidationPassSuccess()
        {
            IValidator validator = new RequiredFieldValidator();
            var result = validator.Validate("-");
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Validate_BlankValue_ValidationFailedSuccess()
        {
            IValidator validator = new RequiredFieldValidator();
            var result = validator.Validate("");
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Validate_BlankNull_ValidtionFailedSuccess()
        {
            IValidator validator = new RequiredFieldValidator();
            var result = validator.Validate(null);
            Assert.AreEqual(false, result);
        }
    }
}
