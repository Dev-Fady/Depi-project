using DEPI_REALESTATE_DB.Model.Config;
using Microsoft.EntityFrameworkCore;

namespace DEPI_REALESTATE_DB.Model
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
        public AppDbContext() : base() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().ToTable("Users", "accounts");
            modelBuilder.Entity<Role>().ToTable("Roles", "accounts");
            modelBuilder.Entity<Agent>().ToTable("Agents", "accounts");
            modelBuilder.Entity<Broker>().ToTable("Brokers", "accounts");

            modelBuilder.Entity<Property>().ToTable("Properties", "pros");
            modelBuilder.Entity<Amenity>().ToTable("Amenities", "pros");



            modelBuilder.Entity<Wishlist>().ToTable("Wishlists", "interactions");
            modelBuilder.Entity<Comment>().ToTable("Comments", "interactions");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoleConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
