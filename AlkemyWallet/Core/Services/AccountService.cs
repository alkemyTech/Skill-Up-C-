using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using AutoMapper;

namespace AlkemyWallet.Core.Services
{
    public class AccountService : IAccountService
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;

        }

        public async Task<IEnumerable<Account>> GetAccounts()
        {
            var accounts = await _unitOfWork.AccountRepository.GetAll();
            return accounts;
        }
        
        public async Task<Account> GetAccountById(int id)
        {
            var account = await _unitOfWork.AccountRepository.GetById(id);
            return account;
        }

    }
}