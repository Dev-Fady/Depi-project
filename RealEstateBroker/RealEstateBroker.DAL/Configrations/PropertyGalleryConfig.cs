using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateBroker.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBroker.DAL.Configrations
{
    internal class PropertyGalleryConfig : IEntityTypeConfiguration<PropertyGallery>
    {
        public void Configure(EntityTypeBuilder<PropertyGallery> builder)
        {
            builder.HasKey(pg => pg.MediaID);

            builder.Property(pg => pg.MediaID)
                .HasDefaultValueSql("NEWID()"); // SQL Server function to generate a new GUID

            builder.Property(Pg => Pg.UploadedAt)
                .HasDefaultValueSql("GETDATE()"); // SQL Server function to get current date and time

            builder.HasOne(pg => pg.Property)
                .WithMany(p => p.PropertyGalleries)
                .HasForeignKey(pg => pg.PropertyID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(pg => pg.ImageURL)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
