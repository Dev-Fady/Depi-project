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
        public AppDbContext() : base() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().ToTable("Users", "accounts");
            modelBuilder.Entity<Role>().ToTable("Roles", "accounts");
            modelBuilder.Entity<Agent>().ToTable("Agents", "accounts");
            modelBuilder.Entity<Broker>().ToTable("Brokers", "accounts");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoleConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
