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
    public class CompoundConfig : IEntityTypeConfiguration<Compound>
    {
        public void Configure(EntityTypeBuilder<Compound> builder)
        {
            builder.HasKey(c => c.CompoundID);

            builder.Property(c => c.CompoundID)
                .HasDefaultValueSql("NEWID()");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Description)
                .HasMaxLength(1000);

            builder.Property(c => c.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasMany(c => c.Properties)
                .WithOne(p => p.Compound)
                .HasForeignKey(p => p.CompoundID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
