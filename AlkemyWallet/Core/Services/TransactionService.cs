using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using AutoMapper;

namespace AlkemyWallet.Core.Services
{
    public class TransactionService : ITransactionService
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
         public TransactionService(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;

        }
        public async Task<IEnumerable<Transaction>> GetTransactions(int userId)
        {
            var transactions = await _unitOfWork.TransactionRepository.GetByUser(userId);
            return transactions;
        }

        public async Task<Transaction> GetTransactionsById(int id, int userId)
        {
            var tran = await _unitOfWork.TransactionRepository.GetById(id);
            if ((tran == null) || (tran.User_id != userId)) return null;
            return tran;
        }
    }
}