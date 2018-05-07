using Pierre.Avenant.Assignment.Core.Entities;

namespace Pierre.Avenant.Assignment.Core.Interfaces.Services
{
    public interface IFileUploadService
    {
        FileImportResult ImportAccountTransactionFile(string importFilePath,string userFileName);
    }
}
