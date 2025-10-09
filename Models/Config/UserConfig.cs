using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AqarakDB.Models.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
           
            builder.HasOne(u => u.Role)
                   .WithMany()
                   .HasForeignKey(u => u.RoleId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.ParentUser)
                   .WithMany(p => p.SubAccounts)
                   .HasForeignKey(u => u.ParentUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(u => u.Phone)
                   .IsUnique(false);
        }
    }
}
