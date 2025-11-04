using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.DAL.Models.Config
{
    public class LikeEntityConfiguration : IEntityTypeConfiguration<LikeEntity>
    {
        public void Configure(EntityTypeBuilder<LikeEntity> builder)
        {
            builder.HasKey(le => le.LikeEntityId);
            builder.Property(le => le.CreatedAt)
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();
            builder.HasOne(le => le.Property)
                     .WithMany(p => p.LikeEntities)
                     .HasForeignKey(le => le.PropertyId)
                     .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(le => le.User)
                     .WithMany(u => u.LikeEntities)
                     .HasForeignKey(le => le.UserID)
                     .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(le => le.Comment)
                    .WithMany(c => c.LikeEntities)
                    .HasForeignKey(le => le.CommentId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
