using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pierre.Avenant.Assignment.Core.Entities;

namespace Pierre.Avenant.Assignment.Web.Models
{
    public class UploadFilesModel
    {
        public List<UploadPartialFailure> RowImportFailures { get; set; }
        public bool Exception { get; set; } = false;
        public string ExceptionMessage { get; set; }
        public string UserFileName { get; set; }
        public int TotalRows { get; set; }
        public int TotalRowsSuccessfull { get; set; }
        public string SuccessMessage { get; set; }
        public int FileUploadId { get; set; }

        public void BuildModelViewFromResult(FileImportResult inputResult)
        {
            TotalRows = inputResult.FileUpload.NumberOfRecords;
            TotalRowsSuccessfull = inputResult.FileUpload.NumberOfSuccessfullRecords;
            FileUploadId = inputResult.FileUpload.FileUploadId;

            if ((inputResult.AccountTransactionUploadFailures?.Count??0) > 0)
            {
                RowImportFailures = new List<UploadPartialFailure>();
                foreach (var inputResultAccountTransactionUploadFailure in inputResult.AccountTransactionUploadFailures)
                {
                    foreach (var transactionAccountFieldFailure in inputResultAccountTransactionUploadFailure
                        .CellValidationFailures)
                    {
                        var f = new UploadPartialFailure()
                        {
                            RowNumber = inputResultAccountTransactionUploadFailure.RowNumber,
                            CollumnName = transactionAccountFieldFailure.ColumnName,
                            ErrorMessage = transactionAccountFieldFailure.ValidationFailureDescription
                        };
                        RowImportFailures.Add(f);
                    }
                }
            }
        }
    }
}
