using System;
using System.Collections.Generic;
using System.Text;

namespace Pierre.Avenant.Assignment.Core.Entities
{
    public class FileImportResult
    {
        public FileUpload FileUpload { get; set; }
        public IList<AccountTransaction> AccountTransactions { get; set; }
        public IList<RowValidationFailure> AccountTransactionUploadFailures { get; set; }
    }
}
