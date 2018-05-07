using System;
using System.Collections.Generic;
using System.Text;
using Pierre.Avenant.Assignment.Core.Interfaces;
using Pierre.Avenant.Assignment.Core.Interfaces.Services;

namespace Pierre.Avenant.Assignment.Core.Services.Validation
{
    public class RequiredFieldCellValidator : ICellValidator
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
