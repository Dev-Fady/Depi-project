using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEPI_PROJECT.DAL.Models.Config
{
    public class BrokerConfiguration : IEntityTypeConfiguration<Broker>
    {
        public void Configure(EntityTypeBuilder<Broker> builder)
        {

            // builder.HasBaseType<User>();
            builder.HasKey(b => b.Id);

            builder.Property(b => b.NationalID)
                   .IsRequired();

            builder.Property(b => b.LicenseID)
                   .IsRequired();

            // Configure foreign key relationship to User
            builder.HasOne(a => a.User)
                   .WithOne(a => a.Broker) // or WithOne() if User should have one Broker
                   .HasForeignKey<Broker>(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
