using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AqarakDB.Models.Config
{
    public class AdConfig : IEntityTypeConfiguration<Ad>
    {
        public void Configure(EntityTypeBuilder<Ad> builder)
        {

            builder.Property(a => a.Description)
                   .HasColumnType("text");

            builder.Property(a => a.Price)
                   .HasColumnType("decimal(18,2)");

            builder.Property(a => a.Type)
                   .HasConversion<string>();

            builder.Property(a => a.Status)
                   .HasConversion<string>();

            builder.HasOne(a => a.User)
                   .WithMany()
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Subscription)
                   .WithMany()
                   .HasForeignKey(a => a.SubscriptionId)
                   .OnDelete(DeleteBehavior.SetNull);

            //builder.HasOne(a => a.AcceptedOffer)
            //       .WithOne(o => o.Ad)
            //       .HasForeignKey<Ad>(a => a.AcceptedOfferId);

            builder.HasMany(a => a.Offers)
                     .WithOne(o => o.Ad)
                     .HasForeignKey(o => o.AdId)
                     .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.ImageAds)
                .WithOne(ai => ai.Ad)
                .HasForeignKey(ia => ia.AdId);

            builder.HasOne(a => a.VideoAd)
                .WithOne(va => va.Ad)
                .HasForeignKey<VideoAd>(va => va.AdId);
        }
    }
}
