using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AqarakDB.Models.Config
{
    public class FavoriteAdConfig : IEntityTypeConfiguration<FavoriteAd>
    {
        public void Configure(EntityTypeBuilder<FavoriteAd> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(fa => fa.Ad)
                   .WithMany(a => a.FavoriteAds)
                   .HasForeignKey(fa => fa.AdId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(fa => fa.User)
                   .WithMany(u => u.FavoriteAds)
                   .HasForeignKey(fa => fa.UserId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasIndex(fa => new { fa.AdId, fa.UserId })
                   .IsUnique();

            builder.HasIndex(fa => fa.UserId);

        }
    }
}
