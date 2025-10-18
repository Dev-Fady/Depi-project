using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEPI_PROJECT.DAL.Models.Config
{
    public class BrokerConfiguration : IEntityTypeConfiguration<Broker>
    {
        public void Configure(EntityTypeBuilder<Broker> builder)
        {

            builder.HasBaseType<User>();

            builder.Property(b => b.NationalID)
                   .IsRequired();

            builder.Property(b => b.LicenseID)
                   .IsRequired();
        }
    }

}
