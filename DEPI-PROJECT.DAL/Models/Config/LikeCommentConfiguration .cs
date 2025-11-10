using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.DAL.Models.Config
{
    public class LikeCommentConfiguration : IEntityTypeConfiguration<LikeComment>
    {
        public void Configure(EntityTypeBuilder<LikeComment> builder)
        {
            builder.HasKey(le => le.LikeCommentId);
            builder.Property(le => le.CreatedAt)
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();
            builder.HasOne(le => le.Comment)
                     .WithMany(c => c.LikeComments)
                     .HasForeignKey(le => le.CommentId)
                     .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(le => le.User)
                     .WithMany(u => u.LikeComments)
                     .HasForeignKey(le => le.UserID)
                     .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(le => new { le.UserID, le.CommentId })
                    .IsUnique();
        }
    }
}
