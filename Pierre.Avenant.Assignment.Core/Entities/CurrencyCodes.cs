using System;
using System.Collections.Generic;
using System.Text;
using Pierre.Avenant.Assignment.Core.Interfaces;
using Pierre.Avenant.Assignment.Core.Interfaces.Database;
using CurrencyCode = System.String;
using Currency = System.String;

namespace Pierre.Avenant.Assignment.Core.Entities
{
    public class CurrencyCodes : Dictionary<CurrencyCode, Currency>
    {
        public ICurrencyCodeRepository _repo;

        public CurrencyCodes(ICurrencyCodeRepository repo)
        {
            _repo = repo;
        }
    }
}
