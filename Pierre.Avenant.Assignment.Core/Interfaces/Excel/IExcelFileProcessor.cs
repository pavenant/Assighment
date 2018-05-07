using System.Collections.Generic;

namespace Pierre.Avenant.Assignment.Core.Interfaces.Excel
{
    public interface IExcelFileProcessor
    {
        IList<(int RowIndex, string Account, string Description, string CurrencyCode, string Amount)> GetTransactionRecords(string filePath);
    }
}
