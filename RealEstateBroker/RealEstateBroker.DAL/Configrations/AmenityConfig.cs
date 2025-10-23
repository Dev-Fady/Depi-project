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
    public class AmenityConfig : IEntityTypeConfiguration<Amenity>
    {
        public void Configure(EntityTypeBuilder<Amenity> builder)
        {
            builder.HasKey(a => a.PropertyID);

            builder.HasOne(a => a.Property)
                .WithOne(p => p.Amenity)
                .HasForeignKey<Amenity>(a => a.PropertyID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(a => a.HasElectricityLine)
                .IsRequired()
                .HasDefaultValue(false);


            builder.Property(a => a.HasWaterLine)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(a => a.HasGasLine)
                .IsRequired()
                .HasDefaultValue(false);
        }   
    }
}
