using System;
using System.Collections.Generic;

namespace Pierre.Avenant.Assignment.Core.Interfaces.Database
{
    public interface ICurrencyCodeRepository
    {
        Dictionary<String, String> GetCurrencyCodes();
        Dictionary<String, String> GetCachedCurrencyCodes();
    }
}