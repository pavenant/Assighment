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

        public bool ValidateUploadRecord((int RowIndex, string Account, string Description, string CurrencyCode, string Amount) uploadRecord, FileImportResult result)
        {
            RowValidationFailure validationFailure = new RowValidationFailure
                {
                    CellValidationFailures = new List<CellValidationFailure>(),
                    RowNumber = uploadRecord.RowIndex + 1
                };

            if (!_requiredValidator.Validate(uploadRecord.Account))
            {
                validationFailure.CellValidationFailures.Add(new CellValidationFailure() { ColumnName = "Account", ValidationFailureDescription = "Required Field" });
            }

            if (!_requiredValidator.Validate(uploadRecord.Description))
            {
                validationFailure.CellValidationFailures.Add(new CellValidationFailure() { ColumnName = "Description", ValidationFailureDescription = "Required Field" });
            }

            if (!_requiredValidator.Validate(uploadRecord.CurrencyCode))
            {
                validationFailure.CellValidationFailures.Add(new CellValidationFailure() { ColumnName = "CurrencyCode", ValidationFailureDescription = "Required Field" });
            }

            if (!_requiredValidator.Validate(uploadRecord.Amount))
            {
                validationFailure.CellValidationFailures.Add(new CellValidationFailure() { ColumnName = "Amount", ValidationFailureDescription = "Required Field" });
            }

            if (!_currencyCodeValidator.Validate(uploadRecord.CurrencyCode))
            {
                validationFailure.CellValidationFailures.Add(new CellValidationFailure() { ColumnName = "CurrencyCode", ValidationFailureDescription = "Currency Code not Valid" });
            }

            if (!_amountValidator.Validate(uploadRecord.Amount))
            {
                validationFailure.CellValidationFailures.Add(new CellValidationFailure() { ColumnName = "Amount", ValidationFailureDescription = "Amount is not a valid amount" });
            }

            if (validationFailure.CellValidationFailures.Count > 0)
            {
                if (result.AccountTransactionUploadFailures == null)
                {
                    result.AccountTransactionUploadFailures = new List<RowValidationFailure>();
                }
                result.AccountTransactionUploadFailures.Add(validationFailure);
                return false;
            }

            return true;
        }

    }
}
