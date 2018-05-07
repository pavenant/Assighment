using System.Collections.Generic;
using Pierre.Avenant.Assignment.Core.Entities;
using Pierre.Avenant.Assignment.Core.Interfaces.Database;

namespace Pierre.Avenant.Assignment.Web.Models
{
    public class AccountTransactionModel
    {
        public int FileUploadId { get; set; }
        public string FileName { get; set; }
        IAccountTransactionRepository _repo;
        public IList<AccountTransaction> AccountTransactions { get; set; }

        public AccountTransactionModel(IAccountTransactionRepository repo,int fileUploadId,string fileName)
        {
            _repo = repo;
            FileUploadId = fileUploadId;
            FileName = fileName;
            AccountTransactions = _repo.LoadAccountTransactionsForFileUploadId(fileUploadId);
        }
    }
}
