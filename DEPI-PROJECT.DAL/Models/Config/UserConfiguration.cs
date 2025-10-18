using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEPI_PROJECT.DAL.Models.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
           
            builder.HasKey(u => u.UserID);

            builder.Property(u => u.UserID)
              .HasDefaultValueSql("NEWID()");

            builder.Property(u => u.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Password)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Phone)
                   .HasMaxLength(20);

            builder.Property(u => u.DateJoined)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(u => u.Role)
                   .WithMany(r => r.Users)
                   .HasForeignKey(u => u.RoleId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
