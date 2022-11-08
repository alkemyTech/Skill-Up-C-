using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;

namespace AlkemyWallet.Repositories
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(WalletDbContext context)
            : base(context)
        {
             
        }

        public async Task<IEnumerable<Transaction>> GetByUser(int userId)
        {
            return await Task.FromResult(_context.Set<Transaction>().Where(t => t.User_id == userId).AsEnumerable());
        }
    }
}

