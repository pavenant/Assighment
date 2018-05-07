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
            ICellValidator cellValidator = new AmountCellValidator();
            var result = cellValidator.Validate("1.100");
            Assert.AreEqual(true,result);
        }

        [TestMethod]
        public void Validate_NotValidDecimal_Failure()
        {
            ICellValidator cellValidator = new AmountCellValidator();
            var result = cellValidator.Validate("notvalid");
            Assert.AreEqual(false, result);
        }
    }
}
