using System.Collections.Generic;
using Pierre.Avenant.Assignment.Core.Entities;

namespace Pierre.Avenant.Assignment.Core.Interfaces.Database
{
    public interface IAccountTransactionRepository
    {
        void Save(AccountTransaction accountTransaction);
        void SaveBulk(IList<AccountTransaction> accountTransaction);
        List<AccountTransaction> LoadAccountTransactionsForFileUploadId(int fileTransactionId);
    }
}
