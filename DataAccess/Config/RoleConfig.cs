using Domin.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Config
{
    //public class RoleConfig : IEntityTypeConfiguration<Role>
    //{
    //    public void Configure(EntityTypeBuilder<Role> builder)
    //    {
    //        builder.HasKey(x => x.ID);
    //        builder.Property(x => x.ID).ValueGeneratedOnAdd();
    //        builder.ToTable("tblRole");
    //        builder.HasMany(x => x.Users)
    //            .WithMany(x => x.Roles)
    //            .UsingEntity<UserRole>();
    //    }
    //}
}
