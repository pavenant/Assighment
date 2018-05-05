using System;
using System.Collections.Generic;
using System.Text;

namespace Pierre.Avenant.Assignment.Core.Entities
{
    public class AccountTransactionImportResult
    {
        public FileUpload FileUpload { get; set; }
        public IList<AccountTransaction> AccountTransactions { get; set; }
        public IList< AccountTransactionUploadFailure> AccountTransactionUploadFailures { get; set; }
    }
}
