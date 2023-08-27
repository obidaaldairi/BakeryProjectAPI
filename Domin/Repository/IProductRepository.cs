using Domin.Entity;

namespace Domin.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public List<Product> Search(string filter = "");
        public int Count();
    }
}
