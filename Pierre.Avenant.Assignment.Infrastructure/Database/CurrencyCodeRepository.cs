using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Pierre.Avenant.Assignment.Core.Entities;
using Pierre.Avenant.Assignment.Core.Interfaces;
using CurrencyCode = System.String;
using Currency = System.String;

namespace Pierre.Avenant.Assignment.Infrastructure.Database
{
    public class CurrencyCodeRepository : ICurrencyCodeRepository
    {
        public Dictionary<CurrencyCode, Currency> GetCurrencyCodes()
        {
            using (IDbConnection connection = new SqlConnection("Data Source=pluto;Initial Catalog=Admanager;uid=trpdev;password=Squeeze66"))
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
    }
}
