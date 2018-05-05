using Pierre.Avenant.Assignment.Core.Entities;

namespace Pierre.Avenant.Assignment.Core.Interfaces.Database
{
    public interface IAccountTransactionRepository
    {
        void Save(AccountTransaction accountTransaction);
    }
}
