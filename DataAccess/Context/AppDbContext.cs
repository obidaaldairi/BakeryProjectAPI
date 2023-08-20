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

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    base.OnConfiguring(optionsBuilder);
        //    var configuration = new ConfigurationBuilder()
        //        .AddJsonFile("appsettings.json")
        //        .Build();
        //    var connectionString = configuration.GetSection("Defult").Value;

        //    optionsBuilder.UseSqlServer(connectionString);
        //}
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        //}

    }
}
