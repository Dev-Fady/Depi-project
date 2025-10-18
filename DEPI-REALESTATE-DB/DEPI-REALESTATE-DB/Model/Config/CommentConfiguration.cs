using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEPI_REALESTATE_DB.Model.Config
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.CommentId);

            builder.Property(c => c.CommentId)
                   .HasDefaultValueSql("NEWID()");

            builder.Property(c => c.CommentText)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(c => c.DateComment)
                   .HasDefaultValueSql("GETDATE()");

            builder.HasOne(c => c.User)
                   .WithMany(u => u.Comments)
                   .HasForeignKey(c => c.UserID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Property)
                   .WithMany(p => p.Comments)
                   .HasForeignKey(c => c.PropertyId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
