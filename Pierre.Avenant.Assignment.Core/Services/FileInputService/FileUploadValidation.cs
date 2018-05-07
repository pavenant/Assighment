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
        private ICellValidator _requiredCellValidator;
        private ICellValidator _amountCellValidator;
        private ICellValidator _currencyCodeCellValidator;

        private ICurrencyCodeRepository _currencyCodeRepository;

        public FileUploadValidation(ICurrencyCodeRepository currencyRepository)
        {
            _currencyCodeRepository = currencyRepository;
            _currencyCodeCellValidator = new CurrencyCodeCellValidator(_currencyCodeRepository);
            _amountCellValidator = new AmountCellValidator();
            _requiredCellValidator = new RequiredFieldCellValidator();
        }

        public bool ValidateUploadRecord((int RowIndex, string Account, string Description, string CurrencyCode, string Amount) uploadRecord, FileImportResult result)
        {
            RowValidationFailure validationFailure = new RowValidationFailure
                {
                    CellValidationFailures = new List<CellValidationFailure>(),
                    RowNumber = uploadRecord.RowIndex + 1
                };

            if (!_requiredCellValidator.Validate(uploadRecord.Account))
            {
                validationFailure.CellValidationFailures.Add(new CellValidationFailure() { ColumnName = "Account", ValidationFailureDescription = "Required Field" });
            }

            if (!_requiredCellValidator.Validate(uploadRecord.Description))
            {
                validationFailure.CellValidationFailures.Add(new CellValidationFailure() { ColumnName = "Description", ValidationFailureDescription = "Required Field" });
            }

            if (!_requiredCellValidator.Validate(uploadRecord.CurrencyCode))
            {
                validationFailure.CellValidationFailures.Add(new CellValidationFailure() { ColumnName = "CurrencyCode", ValidationFailureDescription = "Required Field" });
            }

            if (!_requiredCellValidator.Validate(uploadRecord.Amount))
            {
                validationFailure.CellValidationFailures.Add(new CellValidationFailure() { ColumnName = "Amount", ValidationFailureDescription = "Required Field" });
            }

            if (!_currencyCodeCellValidator.Validate(uploadRecord.CurrencyCode))
            {
                validationFailure.CellValidationFailures.Add(new CellValidationFailure() { ColumnName = "CurrencyCode", ValidationFailureDescription = "Currency Code not Valid" });
            }

            if (!_amountCellValidator.Validate(uploadRecord.Amount))
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
