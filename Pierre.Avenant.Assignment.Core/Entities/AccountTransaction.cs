using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Pierre.Avenant.Assignment.Core.Entities
{
    public class AccountTransaction
    {
        public int AccountTransactionId { get; set; }
        public string Account { get; set; }
        public string Description { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Amount { get; set; }
        public int? FileUploadId { get; set; }
    }
}
