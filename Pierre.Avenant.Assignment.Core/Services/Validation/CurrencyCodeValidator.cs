using System;
using System.Collections.Generic;
using System.Text;
using Pierre.Avenant.Assignment.Core.Interfaces;

namespace Pierre.Avenant.Assignment.Core.Services.Validation
{
    public class CurrencyCodeValidator : IValidator
    {

        public ICurrencyCodeRepository _repo;

        public CurrencyCodeValidator(ICurrencyCodeRepository repo)
        {
            _repo = repo;
        }

        public bool Validate(string input)
        {
            if (_repo.GetCurrencyCodes().ContainsKey(input))
            {
                return true;
            }
            return false;
        }
    }
}
