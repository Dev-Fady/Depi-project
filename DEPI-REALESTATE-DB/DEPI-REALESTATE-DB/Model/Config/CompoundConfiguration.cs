using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEPI_REALESTATE_DB.Model.Config
{
    public class CompoundConfiguration : IEntityTypeConfiguration<Compound>
    {
        public void Configure(EntityTypeBuilder<Compound> builder)
        {

            builder.HasKey(pg => pg.CompoundId);

            builder.Property(pg => pg.CompoundId)
                 .HasDefaultValueSql("NEWID()");

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(c => c.City)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Address)
                   .IsRequired()
                   .HasMaxLength(300);

            builder.Property(c => c.Description)
                   .HasMaxLength(1000);

            builder.HasMany(c => c.Properties)
                   .WithOne(p => p.Compound)
                   .HasForeignKey(p => p.CompoundId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(c => c.City);
        }
    }
}
