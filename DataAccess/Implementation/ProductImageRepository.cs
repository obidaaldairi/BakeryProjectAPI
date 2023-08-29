using DataAccess.Context;
using Domin.Entity;
using Domin.Repository;

namespace DataAccess.Implementation
{
    public class ProductImageRepository : GenericRepository<ProductImages>, IProductImageRepository
    {
        public ProductImageRepository(AppDbContext context) : base(context) { }

    }







}
