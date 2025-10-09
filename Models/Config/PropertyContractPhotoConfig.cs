using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AqarakDB.Models.Config
{
    public class PropertyContractPhotoConfig : IEntityTypeConfiguration<PropertyContractPhoto>
    {
        public void Configure(EntityTypeBuilder<PropertyContractPhoto> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(r => r.Status)
            .HasConversion<string>();

            builder.HasOne(p => p.Ad)
                   .WithMany(a => a.PropertyContractPhotos)
                   .HasForeignKey(p => p.AdId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Reviewer)
                   .WithMany()
                   .HasForeignKey(p => p.ReviewedBy)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }

}
