using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Pierre.Avenant.Assignment.Core.Interfaces.Database;
using CurrencyCode = System.String;
using Currency = System.String;

namespace Pierre.Avenant.Assignment.Infrastructure.Database
{
    public class CurrencyCodeRepository : ICurrencyCodeRepository
    {
        private string _connectionString;
        public CurrencyCodeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private Dictionary<CurrencyCode, Currency> _currencyDictionary;
        static object _lock = new object();

        public Dictionary<CurrencyCode, Currency> GetCurrencyCodes()
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                const string query = @"
	                SET TRAN ISOLATION LEVEL READ UNCOMMITTED

	                select CurrencyCode,Currency from CurrencyCode                                                   
                                     ";

                var result = connection.Query(query).ToDictionary(row => (string)row.CurrencyCode,
                                                                  row => (string)row.Currency);
                if ((result != null) && (result.Any()))
                {
                    return result;
                }
                return null;
            }
        }

        public Dictionary<string, string> GetCachedCurrencyCodes()
        {
            if (_currencyDictionary == null)
            {
                lock (_lock)
                {
                    if (_currencyDictionary == null)
                    {
                        _currencyDictionary = GetCurrencyCodes();
                    }
                }
            }
            return _currencyDictionary;
        }
    }
}
