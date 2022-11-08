using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetTransactions(int userId);
        Task<Transaction> GetTransactionsById(int id, int userId);
    }
}
