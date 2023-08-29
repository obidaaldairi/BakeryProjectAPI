using DataAccess.Context;
using Domin.Repository;
using Microsoft.AspNetCore.Http;

namespace DataAccess.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UnitOfWork(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this._context = context;
            this._httpContextAccessor = httpContextAccessor;
            User = new UserRepository(_context, httpContextAccessor);
            Role = new RoleRepository(_context);
            WebConfiguration = new WebConfigurationRepository(_context);
            UserVerification = new UserVerificationRepository(_context);
            DbInitializer = new DbInitializer(_context);
            CypherServices = new CypherServices();
            Category = new CategoryRepository(_context);
            UserRole = new UserRoleRepository(_context);
            Product = new ProductRepository(_context);
            ProductProvider = new ProductProviderRepository(_context);
            Provider = new ProviderRepository(_context, _httpContextAccessor);
            Admin = new AdminRepository(_context, _httpContextAccessor);
            Cart = new CartRepository(_context);
            ProductImage = new ProductImageRepository(_context);
        }

        public IUserRepository User { get; private set; }

        public IRoleRepository Role { get; private set; }

        public IUserRoleRepository UserRole { get; private set; }

        public IWebConfigurationRepository WebConfiguration { get; private set; }

        public IUserVerificationRepository UserVerification { get; private set; }

        public ICypherServices CypherServices { get; private set; }

        public IDbInitializer DbInitializer { get; private set; }

        public ICategoryRepository Category { get; private set; }

        public IProductRepository Product { get; private set; }

        public IProductProviderRepository ProductProvider { get; private set; }

        public IProviderRepository Provider { get; private set; }

        public IAdminRepository Admin  {get; private set; }

        public ICartRepository Cart { get; private set; }

        public IProductImageRepository ProductImage { get; private set; }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
