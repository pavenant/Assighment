using System;
using System.Collections.Generic;
using System.Text;
using Pierre.Avenant.Assignment.Core.Interfaces;
using Pierre.Avenant.Assignment.Core.Interfaces.Database;
using Pierre.Avenant.Assignment.Core.Interfaces.Services;

namespace Pierre.Avenant.Assignment.Core.Services.Validation
{
    public class CurrencyCodeCellValidator : ICellValidator
    {
        public ICurrencyCodeRepository _repo;

        public CurrencyCodeCellValidator(ICurrencyCodeRepository repo)
        {
            _repo = repo;
        }

        public bool Validate(string input)
        {
            if (string.IsNullOrEmpty( input ))
                return false;

            if (_repo.GetCachedCurrencyCodes().ContainsKey(input))
            {
                return true;
            }
            return false;
        }
    }
}
