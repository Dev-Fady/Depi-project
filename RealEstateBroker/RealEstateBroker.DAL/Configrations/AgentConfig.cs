using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateBroker.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBroker.DAL.Configrations
{
    public class AgentConfig : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            builder.HasBaseType<User>(); // Inherit from User entity --> TPT -->take the base configuration --> primary key, properties

            builder.Property(a => a.AgencyName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(a => a.TaxIdentificationNumber)
                .IsRequired()
                .HasMaxLength(50);
    
            builder.Property(a => a.Rating)
                .HasDefaultValue(0.0f);

            builder.Property(a => a.ExperienceYears)
                .HasDefaultValue(0);

            builder.HasOne<User>()
                   .WithOne()
                   .HasForeignKey<Agent>(a => a.UserID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
