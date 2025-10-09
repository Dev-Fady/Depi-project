using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AqarakDB.Models.Config
{
    public class OfferConfig : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {


            builder.Property(o => o.OfferAmount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(o => o.Message)
                   .HasColumnType("text");

            builder.Property(o => o.Status)
                   .HasConversion<string>()
                   .IsRequired();

            builder.HasOne(o => o.Ad)
                   .WithMany()
                   .HasForeignKey(o => o.AdId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.User)
                   .WithMany()
                   .HasForeignKey(o => o.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
