using System;
using System.Collections.Generic;
using System.Text;
using Pierre.Avenant.Assignment.Core.Interfaces;

namespace Pierre.Avenant.Assignment.Core.Services.Validation
{
    public class ValueSuppliedValidator : IValidator
    {
        public bool Validate(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return true;
        }
    }
}
