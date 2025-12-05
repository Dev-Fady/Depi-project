using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.DAL.Models.Config
{
    public class CommentLikesWithUserViewConfiguration : IEntityTypeConfiguration<CommentLikesWithUserView>
    {
        public void Configure(EntityTypeBuilder<CommentLikesWithUserView> builder)
        {
            builder.HasNoKey();

            builder.Property(c => c.CommentId)
                .HasColumnName("CommentId");

            builder.Property(c => c.LikesCount)
                .HasColumnName("LikesCount");

            builder.Property(c => c.IsLiked)
                .HasColumnType("IsLikedByUser");
        }
    }
}
