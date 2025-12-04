using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEPI_PROJECT.DAL.Models.Config
{
    public class PropertyLikesWithUserViewConfiguration : IEntityTypeConfiguration<PropertyLikesWithUserView>
    {
        public void Configure(EntityTypeBuilder<PropertyLikesWithUserView> builder)
        {
            builder.HasNoKey(); // Views typically don't have primary keys
            
            builder.Property(e => e.PropertyId)
                    .HasColumnName("PropertyId");
            
            builder.Property(e => e.LikesCount)
                    .HasColumnName("LikesCount");
            
            builder.Property(e => e.IsLiked)
                    .HasColumnName("IsLikedByUser");
        }

    }
}