using APIDTOs.DTOs;
using Domin.Entity;

namespace Domin.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public List<Product> Search(string filter = "");
        public int Count();
        List<ProductDTO> getProviderProducts(Guid providerID, Guid userID);
    }
}
