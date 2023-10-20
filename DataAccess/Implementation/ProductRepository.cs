using APIDTOs.DTOs;
using DataAccess.Context;
using Domin.Entity;
using Domin.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementation
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) : base(context) { this._context = context; }

        public int Count()
        {
            return this.FindAllByCondition(q => q.IsDeleted == false).Count();
        }

        public List<ProductDTO> getProviderProducts(Guid providerID, Guid userID)
        {
            var productDto = new List<ProductDTO>();
            var param1 = (providerID != Guid.Empty ? providerID.ToString() : string.Empty);
            var param2 = (userID != Guid.Empty ? userID.ToString() : string.Empty);

            var userIdParameter1 = new SqlParameter("@ProviderID", param1);
            var userIdParameter2 = new SqlParameter("@UserID", param2);

            var products = _context.tblProducts.FromSqlRaw("EXEC GetProviderProducts @ProviderID, @UserID", userIdParameter1, userIdParameter2).ToList();
            productDto.AddRange(products.Cast<ProductDTO>());
            return productDto;
        }

        public List<Product> Search(string filter = "")
        {
            filter = filter.ToLower();
            if (string.IsNullOrEmpty(filter))
            {
                return this.FindAllByCondition(q => q.IsDeleted == false);
            }
            else
            {
                return this.FindAllByCondition(q => q.IsDeleted == false && (q.ArabicProductName.Contains(filter)
                || q.EnglishProductName.Contains(filter)));
            }
        }
    }

}
