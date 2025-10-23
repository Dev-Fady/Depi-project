using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using RealEstateBroker.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBroker.DAL.Configrations
{
    public class CommercialPropertyConfig : IEntityTypeConfiguration<CommercialProperty>
    {
        public void Configure(EntityTypeBuilder<CommercialProperty> builder)
        {
            builder.HasBaseType<Property>(); // Inherit from Property entity --> TPT -->take the base configuration --> primary key, properties

            builder.Property(CP => CP.BusinessType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(CP => CP.FloorNumber)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(CP => CP.HasStorage)
                .IsRequired()
                .HasDefaultValue(0);

            builder.HasOne<Property>()
                .WithOne()
                .HasForeignKey<CommercialProperty>(p => p.PropertyID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
