using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEPI_REALESTATE_DB.Model.Config
{
    public class CommercialPropertyConfiguration : IEntityTypeConfiguration<CommercialProperty>
    {
        public void Configure(EntityTypeBuilder<CommercialProperty> builder)
        {

          
            //builder.HasKey(cp => cp.PropertyId);

            builder.Property(cp => cp.BusinessType)
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.Property(cp => cp.FloorNumber)
                   .IsRequired(false);

            builder.Property(cp => cp.HasStorage)
                   .IsRequired(false);

            builder.HasOne<Property>()
                   .WithOne()
                   .HasForeignKey<CommercialProperty>(cp => cp.PropertyId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
