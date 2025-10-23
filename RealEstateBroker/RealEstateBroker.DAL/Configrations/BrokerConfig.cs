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
    public class BrokerConfig : IEntityTypeConfiguration<Broker>
    {
        public void Configure(EntityTypeBuilder<Broker> builder)
        {
            builder.HasBaseType<User>();

            builder.Property(b => b.NationalID)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(b => b.LicenseID)
                .IsRequired()
                .HasMaxLength(30);


            builder.HasOne<User>()
                   .WithOne()
                   .HasForeignKey<Broker>(a => a.UserID)
                   .OnDelete(DeleteBehavior.Restrict);

        }

    }
}
