using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Pierre.Avenant.Assignment.Core.Entities;
using Pierre.Avenant.Assignment.Core.Interfaces.Database;

namespace Pierre.Avenant.Assignment.Infrastructure.Database
{
    public class AccountTransactionRepository : IAccountTransactionRepository
    {
        public void Save(AccountTransaction accountTransaction)
        {
            using (IDbConnection connection = new SqlConnection("Data Source=localhost;Initial Catalog=Assignment;Integrated Security=True"))
            {
                var sql = @"

	                insert AccountTransaction(Account,Description,CurrencyCode,Amount,FileUploadId)
	                values (@Account,@Description,@CurrencyCode,@Amount,@FileUploadId)
                        
                    select CAST(Scope_Identity() as Int) as Id

                        ";
                accountTransaction.AccountTransactionId = connection.Query<int>(sql, accountTransaction).FirstOrDefault();
            }
        }
    }
}
