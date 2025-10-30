using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEPI_PROJECT.DAL.Models.Config
{
       public class UserConfiguration : IEntityTypeConfiguration<User>
       {
              public void Configure(EntityTypeBuilder<User> builder)
              {
                     builder.HasKey(u => u.UserId);

                     // builder.Ignore(u => u.Id);
                     // builder.Ignore(u => u.TwoFactorEnabled);
                     // // builder.Ignore(u => u.SecurityStamp);
                     // builder.Ignore(u => u.PhoneNumberConfirmed);
                     // builder.Ignore(u => u.LockoutEnd);
                     // builder.Ignore(u => u.LockoutEnabled);
                     
                     builder.Property(u => u.UserName)
                            .IsRequired()
                            .HasMaxLength(100);

                     builder.Property(u => u.Email)
                            .IsRequired()
                            .HasMaxLength(100);

                     builder.Property(u => u.PasswordHash)
                            .IsRequired();


                     builder.Property(u => u.PhoneNumber)
                            .HasMaxLength(20);

                     builder.Property(u => u.DateJoined)
                            .HasDefaultValueSql("GETUTCDATE()");

              }
       }
}
