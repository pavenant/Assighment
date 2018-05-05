using System;
using System.Collections.Generic;
using System.Text;

namespace Pierre.Avenant.Assignment.Core.Entities
{
    public class AccountTransactionUploadFailure
    {
        public int RowNumber { get; set; }
        public IList<TransactionAccountFieldFailure> FieldFailures { get; set; }
    }
}
