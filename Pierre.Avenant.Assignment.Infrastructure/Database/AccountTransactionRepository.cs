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
        private string _connectionString;
        public AccountTransactionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Save(AccountTransaction accountTransaction)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var sql = @"

	                insert AccountTransaction(Account,Description,CurrencyCode,Amount,FileUploadId)
	                values (@Account,@Description,@CurrencyCode,@Amount,@FileUploadId)
                        
                    select CAST(Scope_Identity() as Int) as Id

                        ";
                accountTransaction.AccountTransactionId = connection.Query<int>(sql, accountTransaction).FirstOrDefault();
            }
        }

        public void SaveBulk(IList<AccountTransaction> accountTransaction)
        {
            using (SqlConnection destinationConnection = new SqlConnection(_connectionString))
            {
                destinationConnection.Open();

                using (System.Data.SqlClient.SqlBulkCopy bulkCopy = new System.Data.SqlClient.SqlBulkCopy(destinationConnection))
                {
                    bulkCopy.DestinationTableName = "dbo.AccountTransaction";
                    try
                    {
                        bulkCopy.WriteToServer(accountTransaction.AsDataTable());
                    }
                    catch (Exception ex)
                    {
                        //todo add logging etc
                        throw;
                    }
                }
            }
        }

        //todo add paging 
        public List<AccountTransaction> LoadAccountTransactionsForFileUploadId(int fileTransactionId)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var sql = @"

	                select top 150 Account,Description,CurrencyCode,Amount,FileUploadId from AccountTransaction where FileUploadId = @FileTransactionId
                        
                        ";
                return connection.Query<AccountTransaction>(sql,new { FileTransactionId = fileTransactionId}).ToList();
            }
        }
    }
}
