using DataAccess.Context;
using Domin.Entity;
using Domin.Repository;

namespace DataAccess.Implementation
{
    public class ProviderRepository : GenericRepository<Provider>, IProviderRepository
    {
        public ProviderRepository(AppDbContext context) : base(context) { }

    }








}
