using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEPI_PROJECT.DAL.Models.Config
{
    public class PropertyGalleryConfiguration : IEntityTypeConfiguration<PropertyGallery>
    {
        public void Configure(EntityTypeBuilder<PropertyGallery> builder)
        {

            builder.HasKey(pg => pg.MediaId);

            builder.Property(pg => pg.MediaId)
                 .HasDefaultValueSql("NEWID()");

            builder.HasOne(pg => pg.Property)
                   .WithMany(p => p.PropertyGalleries) 
                   .HasForeignKey(pg => pg.PropertyId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(pg => pg.ImageUrl)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(pg => pg.VideoUrl)
                   .HasMaxLength(500);

            builder.Property(pg => pg.UploadedAt)
                   .HasDefaultValueSql("GETDATE()");

            builder.HasIndex(pg => pg.PropertyId);
        }
    }
}
