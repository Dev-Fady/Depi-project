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
    public class ResidentialPropertyConfig : IEntityTypeConfiguration<ResidentialProperty>
    {
        public void Configure(EntityTypeBuilder<ResidentialProperty> builder)
        {
            builder.HasBaseType<Property>(); // Inherit from Property entity --> TPT -->take the base configuration --> primary key, properties

            builder.Property(RP => RP.Bedrooms)
                .IsRequired()
                .HasDefaultValue(0);
            builder.Property(RP => RP.Bathrooms)
                .IsRequired()
                .HasDefaultValue(0);
            builder.Property(RP => RP.Floors)
                .IsRequired()
                .HasDefaultValue(0);
            builder.Property(RP => RP.KitchenType)
                .IsRequired()
                .HasConversion<string>();

            builder.HasOne<Property>()
                .WithOne()
                .HasForeignKey<ResidentialProperty>( p => p.PropertyID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
