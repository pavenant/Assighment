using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using ExcelDataReader;
using Pierre.Avenant.Assignment.Core.Interfaces;
using Pierre.Avenant.Assignment.Core.Interfaces.Excel;

namespace Pierre.Avenant.Assignment.Infrastructure.Excel
{
    public class ExcelFileProcessor : IExcelFileProcessor
    {
        public IList<(int RowIndex, string Account, string Description, string CurrencyCode, string Amount)> GetTransactionRecords(string filePath)
        {
            var returnRecords = new List<(int RowIndex, string Account, string Description, string CurrencyCode, string Amount)>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            int rowNum = 0;

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        while (reader.Read())
                        {
                            if (rowNum == 0)
                            {
                                ValidateReader(reader);
                            }
                            var sarr = new[] { "", "", "", "" };

                            for (int i = 0; i < 4; i++)
                            {
                                if (reader.FieldCount > i)
                                {
                                    sarr[i] = reader[i]?.ToString();
                                }
                            }
                            returnRecords.Add((rowNum, sarr[0], sarr[1], sarr[2], sarr[3]));
                            rowNum++;
                        }
                    } while (reader.NextResult());
                }
            }
            return returnRecords;
        }

        private void ValidateReader(IExcelDataReader reader)
        {
            ValidateRows(reader);
            ValidateHeader(reader);
        }

        private void ValidateRows(IExcelDataReader reader)
        {
            if (reader.RowCount < 2)
            {
                throw new FormatException(
                    "Invalid File Format. Expecting File Header (Account,Description,CurrencyCode,Amount) and at least 1 valid transaction row.");
            }
        }

        private static void ValidateHeader(IExcelDataReader reader)
        {
            ValidateHeaderFieldCount(reader);
            ValidateHeaderAccount(reader);
            ValidateHeaderDescription(reader);
            ValidateHeaderCurrencyCode(reader);
            ValidateHeaderAmount(reader);
        }

        private static void ValidateHeaderAmount(IExcelDataReader reader)
        {
            if (reader[3] == null || reader[3].ToString().ToUpper() != "AMOUNT")
            {
                throw new FormatException(
                    "Header not valid. Expected Header Columns (Account,Description,CurrencyCode,Amount)");
            }
        }

        private static void ValidateHeaderCurrencyCode(IExcelDataReader reader)
        {
            if (reader[2] == null || reader[2].ToString().ToUpper().Replace(" ", "") != "CURRENCYCODE")
            {
                throw new FormatException(
                    "Header not valid. Expected Header Columns (Account,Description,CurrencyCode,Amount)");
            }
        }

        private static void ValidateHeaderDescription(IExcelDataReader reader)
        {
            if (reader[1] == null || reader[1].ToString().ToUpper() != "DESCRIPTION")
            {
                throw new FormatException(
                    "Header not valid. Expected Header Columns (Account,Description,CurrencyCode,Amount)");
            }
        }

        private static void ValidateHeaderAccount(IExcelDataReader reader)
        {
            if (reader[0] == null || reader[0].ToString().ToUpper() != "ACCOUNT")
            {
                throw new FormatException(
                    "Header not valid. Expected Header Columns (Account,Description,CurrencyCode,Amount)");
            }
        }

        private static void ValidateHeaderFieldCount(IExcelDataReader reader)
        {
            if (reader.FieldCount != 4)
            {
                throw new FormatException(
                    "Header not valid. Expected Header Columns (Account,Description,CurrencyCode,Amount)");
            }
        }
    }
}
