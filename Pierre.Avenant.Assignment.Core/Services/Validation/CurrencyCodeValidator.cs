﻿using System;
using System.Collections.Generic;
using System.Text;
using Pierre.Avenant.Assignment.Core.Interfaces;
using Pierre.Avenant.Assignment.Core.Interfaces.Database;
using Pierre.Avenant.Assignment.Core.Interfaces.Services;

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
            if (_repo.GetCachedCurrencyCodes().ContainsKey(input))
            {
                return true;
            }
            return false;
        }
    }
}
