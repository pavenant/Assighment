using System;
using System.Collections.Generic;

namespace Pierre.Avenant.Assignment.Core.Interfaces
{
    public interface ICurrencyCodeRepository
    {
        Dictionary<String, String> GetCurrencyCodes();
    }
}