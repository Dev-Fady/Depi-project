using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEPI_REALESTATE_DB.Model.Config
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {

            builder.HasKey(p => p.PropertyId);

            builder.Property(p => p.PropertyId)
                .HasDefaultValueSql("NEWID()");

            builder.Property(p => p.City)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Address)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(p => p.GoogleMapsUrl)
                   .HasMaxLength(500);

            builder.Property(p => p.Price)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(p => p.Square)
                   .IsRequired();

            builder.Property(p => p.Description)
                   .HasMaxLength(1000);

            builder.Property(p => p.DateListed)
                   .HasDefaultValueSql("GETDATE()"); 

            builder.HasOne(p => p.Agent)
                   .WithMany(a => a.Properties)
                   .HasForeignKey(p => p.AgentId)
                   .OnDelete(DeleteBehavior.Restrict); 

            builder.Property(p => p.PropertyType)
                   .HasConversion<string>();

            builder.Property(p => p.PropertyPurpose)
                   .HasConversion<string>();

            builder.Property(p => p.PropertyStatus)
                   .HasConversion<string>();
        }
    }
}
