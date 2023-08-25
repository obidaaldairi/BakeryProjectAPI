using Domin.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options) { }

        public DbSet<User> tblUsers { get; set; }
        public DbSet<Role> tblRoles { get; set; }
        public DbSet<UserRole> tblUserRoles { get; set; }
        public DbSet<WebConfiguration> tblWebConfigurations { get; set; }
        public DbSet<UserVerification> tblUserVerification { get; set; }
        public DbSet<Category> tblCategories { get; set; }
        public DbSet<Cart> tblCarts { get; set; }
        public DbSet<Product> tblProducts { get; set; }
        public DbSet<ProductImages> tblProductImages { get; set; }
        public DbSet<UserInformation> tblUserInformations { get; set; }
        public DbSet<Provider> tblProviders { get; set; }
        public DbSet<ProductProvider> tblProductProviders { get; set; }
        public DbSet<Admin> tblAdmins { get; set; }

    }
}
