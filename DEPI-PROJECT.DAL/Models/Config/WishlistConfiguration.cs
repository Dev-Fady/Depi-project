using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEPI_PROJECT.DAL.Models.Config
{
    public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {

            builder.HasKey(l => l.ListingID);

            builder.Property(l => l.ListingID)
              .HasDefaultValueSql("NEWID()");

            builder.Property(l=> l.CreatedAt)
                   .HasDefaultValueSql("GETDATE()");

            builder.HasOne(l => l.User)
              .WithMany(u => u.Wishlists)
              .HasForeignKey(l => l.UserID)
              .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(l => l.Property)
                   .WithMany(p => p.Wishlists)
                   .HasForeignKey(l => l.PropertyID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(l => new { l.UserID, l.PropertyID })
                   .IsUnique();
        }
    }
}
