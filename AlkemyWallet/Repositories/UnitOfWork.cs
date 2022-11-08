using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;

namespace AlkemyWallet.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Basicamente unit of work tiene la funcion de crear funciones sincronicas, por ejemplo : al transferir dinero de una cuenta
        /// bancaria a otra, las dos cuentas bancarias sufren un cambio en sus depositos de forma simultanea, unit of work se encarga de enlazar
        /// las dos tablas o mas para asi utilizar una sola instancia de DBcontext.
        /// https://www.youtube.com/watch?v=GdRBFc_KKKM este video de la tutoria les permitira tener un ejemplo de en que momento usar
        /// la sincronizacion.(tambien para ver como initiliazar el context en con el unit of work en los endpoints).
        /// </summary>
        private readonly WalletDbContext _dbContext;
        private readonly IRepositoryBase<User> userRepository;
        private readonly IRepositoryBase<Account> accountRepository;
        private readonly ITransactionRepository transactionRepository;
        private readonly IRepositoryBase<FixedTermDeposit> fixedTermDepositRepository;
        private readonly IRepositoryBase<Role> roleRepository;
        private readonly IRepositoryBase<Catalogue> catalogueRepository;
        public UnitOfWork(WalletDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IRepositoryBase<User> UserRepository => userRepository ?? new RepositoryBase<User>(_dbContext);

        public IRepositoryBase<Account> AccountRepository => accountRepository ?? new RepositoryBase<Account>(_dbContext);

        public ITransactionRepository TransactionRepository => transactionRepository ?? new TransactionRepository(_dbContext);

        public IRepositoryBase<FixedTermDeposit> FixedTermDepositRepository => fixedTermDepositRepository ?? new RepositoryBase<FixedTermDeposit>(_dbContext);

        public IRepositoryBase<Role> RoleRepository => roleRepository ?? new RepositoryBase<Role>(_dbContext);

        public IRepositoryBase<Catalogue> CatalogueRepository => catalogueRepository ?? new RepositoryBase<Catalogue>(_dbContext);

        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
           await _dbContext.SaveChangesAsync();
        }
    }
}
