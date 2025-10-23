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
    public class WishListConfig : IEntityTypeConfiguration<WishList>
    {
        public void Configure(EntityTypeBuilder<WishList> builder)
        {
            builder.HasKey(w => w.ListingID);

            builder.Property(w => w.ListingID)
                .HasDefaultValueSql("NEWID()"); // SQL Server function to generate a new GUID

            builder.Property(W=>W.CreatedAt)
                .HasDefaultValueSql("GETDATE()"); // SQL Server function to get current date and time

            builder.HasOne(W => W.User)
                   .WithMany(U=> U.WishLists)
                   .HasForeignKey(W => W.UserID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(W => W.Property)
                   .WithMany(P => P.WishLists)
                   .HasForeignKey(W => W.PropertyID)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
