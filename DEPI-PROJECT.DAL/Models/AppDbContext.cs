using DEPI_PROJECT.DAL.Models.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace DEPI_PROJECT.DAL.Models
{
    public class AppDbContext : IdentityDbContext<User, Role, Guid>
    {
        // Users and Roles DbSets are inherited from IdentityDbContext
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
        //public DbSet<LikeEntity> LikeEntities { get; set; }
        public AppDbContext() : base() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call base first to configure Identity tables
            base.OnModelCreating(modelBuilder);

            // ----- Accounts Schema -----
            modelBuilder.Entity<User>().ToTable("Users", "accounts");
            modelBuilder.Entity<Role>().ToTable("Roles", "accounts");
            modelBuilder.Entity<Agent>().ToTable("Agents", "accounts");
            modelBuilder.Entity<Broker>().ToTable("Brokers", "accounts");

            // Move Identity tables to accounts schema
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims", "accounts");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins", "accounts");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens", "accounts");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles", "accounts");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims", "accounts");

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
            //modelBuilder.Entity<LikeEntity>().ToTable("LikeEntities", "interactions");


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoleConfiguration).Assembly);
        }
    }
}
