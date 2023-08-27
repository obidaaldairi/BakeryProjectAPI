using DataAccess.Context;
using Domin.Entity;
using Domin.Repository;

namespace DataAccess.Implementation
{
    public class ProductProviderRepository : GenericRepository<ProductProvider>, IProductProviderRepository
    {
        public ProductProviderRepository(AppDbContext context) : base(context) { }



    }







}
