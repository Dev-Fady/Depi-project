using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEPI_REALESTATE_DB.Model.Config
{
    public class AgentConfiguration : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            // TPT inheritance => Key is the same as UserID
            builder.HasBaseType<User>();

            builder.Property(a => a.AgencyName)
                   .HasMaxLength(100);

            builder.Property(a => a.TaxIdentificationNumber)
                   .IsRequired();

            builder.Property(a => a.Rating)
                   .HasDefaultValue(0);

            builder.Property(a => a.ExperienceYears)
                   .HasDefaultValue(0);
        }
    }
}
