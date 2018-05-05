using System;
using System.Collections.Generic;
using System.Text;
using Pierre.Avenant.Assignment.Core.Interfaces;
using Pierre.Avenant.Assignment.Core.Interfaces.Services;

namespace Pierre.Avenant.Assignment.Core.Services.Validation
{
    public class AmountValidator : IValidator
    {
        public bool Validate(string input)
        {
            if (decimal.TryParse(input,out _))
            {
                return true;
            }

            return false;
        }
    }
}
