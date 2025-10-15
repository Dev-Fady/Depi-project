using DEPI_REALESTATE_DB.Model.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEPI_REALESTATE_DB.Model.Config
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
              .HasDefaultValueSql("NEWID()");

            builder.Property(r => r.RoleType)
                   .IsRequired();

            builder.Ignore(r => r.RoleName);

            builder.HasMany(r => r.Users)
                   .WithOne(u => u.Role)
                   .HasForeignKey(u => u.RoleId)
                   .OnDelete(DeleteBehavior.Restrict);

            ////  Seed Data
            //builder.HasData(
            //    new Role
            //    {
            //        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            //        RoleType = UserRole.Admin
            //    },
            //    new Role
            //    {
            //        Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            //        RoleType = UserRole.Agent
            //    },
            //    new Role
            //    {
            //        Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            //        RoleType = UserRole.Broker
            //    },
            //    new Role
            //    {
            //        Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
            //        RoleType = UserRole.Customer
            //    }
            //);
        }
    }
}
