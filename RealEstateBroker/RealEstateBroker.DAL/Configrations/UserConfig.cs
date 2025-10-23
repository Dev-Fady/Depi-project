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
    internal class UserConfig : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserID);

            builder.Property(u => u.UserID)
                .HasDefaultValueSql("NEWID()"); // SQL Server function to generate a new GUID

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(u => u.Phone)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(u => u.Role)
                .IsRequired()
                .HasComment("0: Admin, 1: Agent, 2: Customer") // Enum mapping comment
            .HasConversion<string>();   

            builder.Property(u => u.DateJoined)
                .HasDefaultValueSql("GETDATE()"); // SQL Server function to get current date and time

        }
    }
}
