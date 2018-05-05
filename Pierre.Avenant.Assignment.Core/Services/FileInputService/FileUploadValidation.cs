using System;
using System.Collections.Generic;
using System.Text;
using Pierre.Avenant.Assignment.Core.Entities;
using Pierre.Avenant.Assignment.Core.Interfaces.Database;
using Pierre.Avenant.Assignment.Core.Interfaces.Excel;
using Pierre.Avenant.Assignment.Core.Interfaces.Services;
using Pierre.Avenant.Assignment.Core.Services.Validation;

namespace Pierre.Avenant.Assignment.Core.Services.FileInputService
{
    public class FileUploadValidation
    {
        private IValidator _requiredValidator;
        private IValidator _amountValidator;
        private IValidator _currencyCodeValidator;

        private ICurrencyCodeRepository _currencyCodeRepository;

        public FileUploadValidation(ICurrencyCodeRepository currencyRepository)
        {
            _currencyCodeRepository = currencyRepository;
            _currencyCodeValidator = new CurrencyCodeValidator(_currencyCodeRepository);
            _amountValidator = new AmountValidator();
            _requiredValidator = new RequiredFieldValidator();
        }

        public bool ValidateUploadRecord((int RowIndex, string Account, string Description, string CurrencyCode, string Amount) uploadRecord, AccountTransactionImportResult result)
        {
            AccountTransactionUploadFailure failure =
                new AccountTransactionUploadFailure
                {
                    FieldFailures = new List<TransactionAccountFieldFailure>(),
                    RowNumber = uploadRecord.RowIndex + 1
                };

            if (!_requiredValidator.Validate(uploadRecord.Account))
            {
                failure.FieldFailures.Add(new TransactionAccountFieldFailure() { FieldName = "Account", FailureDescription = "Required Field" });
            }

            if (!_requiredValidator.Validate(uploadRecord.Description))
            {
                failure.FieldFailures.Add(new TransactionAccountFieldFailure() { FieldName = "Description", FailureDescription = "Required Field" });
            }

            if (!_requiredValidator.Validate(uploadRecord.CurrencyCode))
            {
                failure.FieldFailures.Add(new TransactionAccountFieldFailure() { FieldName = "CurrencyCode", FailureDescription = "Required Field" });
            }

            if (!_requiredValidator.Validate(uploadRecord.Amount))
            {
                failure.FieldFailures.Add(new TransactionAccountFieldFailure() { FieldName = "Amount", FailureDescription = "Required Field" });
            }

            if (!_currencyCodeValidator.Validate(uploadRecord.CurrencyCode))
            {
                failure.FieldFailures.Add(new TransactionAccountFieldFailure() { FieldName = "CurrencyCode", FailureDescription = "Currency Code not Valid" });
            }

            if (!_amountValidator.Validate(uploadRecord.Amount))
            {
                failure.FieldFailures.Add(new TransactionAccountFieldFailure() { FieldName = "Amount", FailureDescription = "Amount is not a valid amount" });
            }

            if (failure.FieldFailures.Count > 0)
            {
                if (result.AccountTransactionUploadFailures == null)
                {
                    result.AccountTransactionUploadFailures = new List<AccountTransactionUploadFailure>();
                }
                result.AccountTransactionUploadFailures.Add(failure);
                return false;
            }

            return true;
        }

    }
}
