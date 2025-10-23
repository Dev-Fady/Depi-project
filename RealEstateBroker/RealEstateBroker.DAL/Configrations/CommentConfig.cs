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
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.CommentID);

            builder.Property(c => c.CommentID)
                .HasDefaultValueSql("NEWID()"); // SQL Server function to generate a new GUID

            builder.HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Property)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.PropertyID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
