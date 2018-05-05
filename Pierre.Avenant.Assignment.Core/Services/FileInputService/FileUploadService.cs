using System.Collections.Generic;
using System.Data;
using System.IO;
using Pierre.Avenant.Assignment.Core.Entities;
using Pierre.Avenant.Assignment.Core.Interfaces.Database;
using Pierre.Avenant.Assignment.Core.Interfaces.Excel;
using Pierre.Avenant.Assignment.Core.Interfaces.Services;

namespace Pierre.Avenant.Assignment.Core.Services.FileInputService
{
    public class FileUploadService : IFileUploadService
    {
        private IExcelFileLoader _excelFileLoader;
        private FileUploadValidation _fileUploadValidation;
        private IAccountTransactionRepository _accountTransactionRepository;
        private IFileUploadRepository _fileUploadRepository;


        public FileUploadService(IExcelFileLoader excelFileLoader,ICurrencyCodeRepository currencyRepository,IFileUploadRepository fileUploadRepository, IAccountTransactionRepository accountTransactionRepository)
        {
            _excelFileLoader = excelFileLoader;
            _accountTransactionRepository = accountTransactionRepository;
            _fileUploadRepository = fileUploadRepository;
            _fileUploadValidation = new FileUploadValidation(currencyRepository);
        }

        public AccountTransactionImportResult InportAccountTransactionFile(string filePath)
        {
            var accountTransactionImportResult = ProcessTranactionsAccountFile(filePath, out var fileUploadRecords);

            SetFileNameData(filePath, accountTransactionImportResult, fileUploadRecords);

            //persist data.
            PersistTransactionImportResult(accountTransactionImportResult);
            
            return accountTransactionImportResult;
        }

        private void PersistTransactionImportResult(AccountTransactionImportResult accountTransactionImportResult)
        {
            _fileUploadRepository.Save(accountTransactionImportResult.FileUpload);
            foreach (var accountTransaction in accountTransactionImportResult.AccountTransactions)
            {
                accountTransaction.FileUploadId = accountTransactionImportResult.FileUpload.FileUploadId;
                _accountTransactionRepository.Save(accountTransaction);
            }
        }

        private AccountTransactionImportResult ProcessTranactionsAccountFile(string filePath, out IList<(int RowIndex, string Account, string Description, string CurrencyCode, string Amount)> fileUploadRecords)
        {
            AccountTransactionImportResult accountTransactionImportResult = new AccountTransactionImportResult();

            fileUploadRecords = _excelFileLoader.GetTransactionRecords(filePath);

            foreach (var uploadRecord in fileUploadRecords)
            {
                if (!IsHeader(uploadRecord))
                {
                    if (_fileUploadValidation.ValidateUploadRecord(uploadRecord, accountTransactionImportResult))
                    {
                        AddAccountTransaction(uploadRecord, accountTransactionImportResult);
                    }
                }
            }

            return accountTransactionImportResult;
        }

        private static void SetFileNameData(string filePath, AccountTransactionImportResult accountTransactionImportResult,
            IList<(int RowIndex, string Account, string Description, string CurrencyCode, string Amount)> fileUploadRecords)
        {
            accountTransactionImportResult.FileUpload = new FileUpload();
            accountTransactionImportResult.FileUpload.NumberOfRecords = fileUploadRecords.Count - 1;
            if (accountTransactionImportResult.AccountTransactions != null)
            {
                accountTransactionImportResult.FileUpload.NumberOfSuccessfullRecords =
                    accountTransactionImportResult.AccountTransactions.Count;
            }

            accountTransactionImportResult.FileUpload.FileName = Path.GetFileName(filePath);
        }

        private static void AddAccountTransaction(
            (int RowIndex, string Account, string Description, string CurrencyCode, string Amount) uploadRecord,
            AccountTransactionImportResult result)
        {
            AccountTransaction accountTransaction = new AccountTransaction()
            {
                Account = uploadRecord.Account,
                Description = uploadRecord.Description,
                CurrencyCode = uploadRecord.CurrencyCode,
                Amount = decimal.Parse(uploadRecord.Amount)
            };
            if (result.AccountTransactions == null)
            {
                result.AccountTransactions = new List<AccountTransaction>();
            }

            result.AccountTransactions.Add(accountTransaction);
        }

        private static bool IsHeader((int RowIndex, string Account, string Description, string CurrencyCode, string Amount) uploadRecord)
        {
            return uploadRecord.RowIndex == 0;
        }
    }
}
