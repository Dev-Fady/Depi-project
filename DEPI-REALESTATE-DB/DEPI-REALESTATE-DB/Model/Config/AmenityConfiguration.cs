using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEPI_REALESTATE_DB.Model.Config
{
    public class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
    {
        public void Configure(EntityTypeBuilder<Amenity> builder)
        {
         
            builder.HasKey(a => a.PropertyId);

            builder.HasOne(a => a.Property)
                   .WithOne(p => p.Amenity)
                   .HasForeignKey<Amenity>(a => a.PropertyId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(a => a.HasElectricityLine)
                   .HasDefaultValue(false);

            builder.Property(a => a.HasWaterLine)
                   .HasDefaultValue(false);

            builder.Property(a => a.HasGasLine)
                   .HasDefaultValue(false);
        }
    }
}
