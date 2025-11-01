using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DEPI_PROJECT.DAL.Models.Config
{
    public class AgentConfiguration : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            // Remove inheritance - Agent is now a standalone entity
            // Configure primary key
            builder.HasKey(a => a.Id);

            // Configure properties
            builder.Property(a => a.AgencyName)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(a => a.TaxIdentificationNumber)
                   .IsRequired();

            builder.Property(a => a.Rating)
                   .HasDefaultValue(0);

            builder.Property(a => a.ExperienceYears)
                   .HasDefaultValue(0);

            // Configure foreign key relationship to User
            builder.HasOne(a => a.User)
                   .WithOne(a => a.Agent) // or WithOne() if User should have one Agent
                   .HasForeignKey<Agent>(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
