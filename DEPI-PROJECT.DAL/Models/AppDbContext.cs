using DEPI_PROJECT.DAL.Models.Config;
using Microsoft.EntityFrameworkCore;

namespace DEPI_PROJECT.DAL.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Broker> Brokers { get; set; }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Amenity> Amenities { get; set; }

        public DbSet<PropertyGallery> PropertyGalleries { get; set; }
        public DbSet<CommercialProperty> CommercialProperties { get; set; }
        public DbSet<Compound> Compounds { get; set; }
        public DbSet<ResidentialProperty> ResidentialProperties {  get; set; }
        public AppDbContext() : base() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ----- Accounts Schema -----
            modelBuilder.Entity<User>().ToTable("Users", "accounts");
            modelBuilder.Entity<Role>().ToTable("Roles", "accounts");
            modelBuilder.Entity<Agent>().ToTable("Agents", "accounts");
            modelBuilder.Entity<Broker>().ToTable("Brokers", "accounts");

            // ----- Properties (Pros) Schema -----
            modelBuilder.Entity<Property>().ToTable("Properties", "pros");
            modelBuilder.Entity<Amenity>().ToTable("Amenities", "pros");
            modelBuilder.Entity<PropertyGallery>().ToTable("PropertyGalleries", "pros");
            modelBuilder.Entity<CommercialProperty>().ToTable("CommercialProperties", "pros");
            modelBuilder.Entity<Compound>().ToTable("Compounds", "pros");
            modelBuilder.Entity<ResidentialProperty>().ToTable("ResidentialProperties", "pros");

            // ----- Interactions Schema -----
            modelBuilder.Entity<Wishlist>().ToTable("Wishlists", "interactions");
            modelBuilder.Entity<Comment>().ToTable("Comments", "interactions");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoleConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
