using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEPI_PROJECT.DAL.Models.Config
{
    public class ResidentialPropertyConfiguration : IEntityTypeConfiguration<ResidentialProperty>
    {
        public void Configure(EntityTypeBuilder<ResidentialProperty> builder)
        {
            //builder.HasKey(rp => rp.PropertyId);

            builder.Property(rp => rp.Bedrooms)
                   .IsRequired();

            builder.Property(rp => rp.Bathrooms)
                   .IsRequired();

            builder.Property(rp => rp.Floors)
                   .IsRequired(false);

            builder.Property(rp => rp.KitchenType)
                   .HasConversion<string>() 
                   .IsRequired();

            builder.HasOne<Property>()
                   .WithOne()
                   .HasForeignKey<ResidentialProperty>(rp => rp.PropertyId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
