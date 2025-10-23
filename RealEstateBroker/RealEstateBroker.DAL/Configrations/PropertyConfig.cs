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
    public class PropertyConfig : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasKey(p => p.PropertyID);

            builder.Property(p => p.PropertyID)
                .HasDefaultValueSql("NEWID()");

            builder.Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            builder.Property(p => p.City)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(p => p.GoogleMapsURL)
                    .IsRequired()
                    .HasMaxLength(500);


            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)"); // Define precision and scale for decimal

            builder.Property(p=>p.PropertyType)
                .IsRequired()
                .HasComment("0: Sale, 1: Rent") // Enum mapping comment
                .HasConversion<string>();

            builder.Property(p => p.PropertyPurpose)
                .IsRequired()
                .HasComment("0: Residential, 1: Commercial") // Enum mapping comment
                .HasConversion<string>();

            builder.Property(p => p.PropertyStatus)
                .IsRequired()
                .HasComment("0: Available, 1: Sold, 2: Rented") // Enum mapping comment
                .HasConversion<string>();

            builder.Property(p => p.Square)
                .IsRequired();

            builder.Property(p => p.DateListed)
                .HasDefaultValueSql("GETDATE()"); // SQL Server function to get current date and time

            builder.HasOne(P => P.Agent)
                .WithMany(A => A.Properties)
                .HasForeignKey(P => P.AgentID)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
