using System;
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
        private IExcelFileProcessor _excelFileProcessor;
        private FileUploadValidation _fileUploadValidation;
        private IAccountTransactionRepository _accountTransactionRepository;
        private IFileUploadRepository _fileUploadRepository;

        public FileUploadService(IExcelFileProcessor excelFileProcessor,ICurrencyCodeRepository currencyRepository,IFileUploadRepository fileUploadRepository, IAccountTransactionRepository accountTransactionRepository)
        {
            _excelFileProcessor = excelFileProcessor;
            _accountTransactionRepository = accountTransactionRepository;
            _fileUploadRepository = fileUploadRepository;
            _fileUploadValidation = new FileUploadValidation(currencyRepository);
        }

        public FileImportResult ImportAccountTransactionFile(string importFilePath,string userFilePath)
        {
            var accountTransactionImportResult = ProcessTranactionsAccountFile(importFilePath, out var fileUploadRecords);
            SetFileNameData(importFilePath, userFilePath, accountTransactionImportResult, fileUploadRecords);
            PersistTransactionImportResult(accountTransactionImportResult);
            return accountTransactionImportResult;
        }

        private void PersistTransactionImportResult(FileImportResult fileImportResult)
        {
            _fileUploadRepository.Save(fileImportResult.FileUpload);
            foreach (var accountTransaction in fileImportResult.AccountTransactions)
            {
                accountTransaction.FileUploadId = fileImportResult.FileUpload.FileUploadId;
            }

            //to enable large files, use bulk upload --> SQL Bulk Copy is not .net core complient, TODO review compatible solution
            _accountTransactionRepository.SaveBulk(fileImportResult.AccountTransactions);

            //foreach (var accountTransaction in fileImportResult.AccountTransactions)
            //{
            //    accountTransaction.FileUploadId = fileImportResult.FileUpload.FileUploadId;
            //    _accountTransactionRepository.Save(accountTransaction);
            //}
        }

        private FileImportResult ProcessTranactionsAccountFile(string filePath, out IList<(int RowIndex, string Account, string Description, string CurrencyCode, string Amount)> fileUploadRecords)
        {
            FileImportResult fileImportResult = new FileImportResult();

            fileUploadRecords = _excelFileProcessor.GetTransactionRecords(filePath);

            foreach (var uploadRecord in fileUploadRecords)
            {
                if (!IsHeader(uploadRecord))
                {
                    if (_fileUploadValidation.ValidateUploadRecord(uploadRecord, fileImportResult))
                    {
                        AddAccountTransaction(uploadRecord, fileImportResult);
                    }
                }
            }
            return fileImportResult;
        }

        private static void SetFileNameData(string filePath,string userFilePath, FileImportResult fileImportResult,
            IList<(int RowIndex, string Account, string Description, string CurrencyCode, string Amount)> fileUploadRecords)
        {
            fileImportResult.FileUpload = new FileUpload
            {
                NumberOfRecords = fileUploadRecords.Count - 1
            };
            if (fileImportResult.AccountTransactions != null)
            {
                fileImportResult.FileUpload.NumberOfSuccessfullRecords = fileImportResult.AccountTransactions.Count;
            }
            fileImportResult.FileUpload.FileName = Path.GetFileName(userFilePath);
        }

        private static void AddAccountTransaction(
            (int RowIndex, string Account, string Description, string CurrencyCode, string Amount) uploadRecord,
            FileImportResult result)
        {
            AccountTransaction accountTransaction = new AccountTransaction()
            {
                Account = uploadRecord.Account,
                Description = uploadRecord.Description,
                CurrencyCode = uploadRecord.CurrencyCode,
                Amount = decimal.Parse(uploadRecord.Amount),
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
