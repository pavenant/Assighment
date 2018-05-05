using Pierre.Avenant.Assignment.Core.Entities;

namespace Pierre.Avenant.Assignment.Core.Interfaces.Services
{
    public interface IFileUploadService
    {
        AccountTransactionImportResult InportAccountTransactionFile(string filePath);
    }
}
