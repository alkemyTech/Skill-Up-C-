using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{

    public interface ITransactionRepository : IRepositoryBase<Transaction>
    {
        Task<IEnumerable<Transaction>> GetByUser(int userId);

    }
}
