using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;

namespace AlkemyWallet.Repositories
{
    public class CatalogueRepository : RepositoryBase<Catalogue>, ICatalogueRepository
    {
        public CatalogueRepository(WalletDbContext context)
            : base(context)
        {

        }


    }
}

