using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AqarakDB.Models.Config
{
    public class SaveAdConfig : IEntityTypeConfiguration<SaveAd>
    {
        public void Configure(EntityTypeBuilder<SaveAd> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(sa => sa.Ad)
                   .WithMany(a => a.SaveAds)
                   .HasForeignKey(fa => fa.AdId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sa => sa.User)
                   .WithMany(u => u.SaveAds)
                   .HasForeignKey(fa => fa.UserId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.HasIndex(fa => new { fa.AdId, fa.UserId })
                   .IsUnique();

            builder.HasIndex(fa => fa.UserId);

        }
    }
}
