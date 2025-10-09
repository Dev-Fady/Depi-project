using AqarakDB.Models.Config;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace AqarakDB.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Offer> offers { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Subscription> subscriptions { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<ImageAd> ImageAds { get; set; }
        public DbSet<VideoAd> VideoAds { get; set; }
        public DbSet<FavoriteAd> FavoriteAds { get; set; }
        public DbSet<SaveAd> SaveAds { get; set; }
        public DbSet<PropertyContractPhoto> PropertyContractPhotos { get; set; }

        public AppDbContext() : base() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // auth schema
            modelBuilder.Entity<User>().ToTable("Users", "auth");
            modelBuilder.Entity<Role>().ToTable("Roles", "auth");

            //// billing schema
            modelBuilder.Entity<Subscription>().ToTable("Subscriptions", "billing");
            modelBuilder.Entity<Payment>().ToTable("Payments", "billing");

            //// ads schema
            modelBuilder.Entity<Ad>().ToTable("Ads", "ads");
            modelBuilder.Entity<ImageAd>().ToTable("ImageAds", "ads");
            modelBuilder.Entity<VideoAd>().ToTable("VideoAds", "ads");
            modelBuilder.Entity<FavoriteAd>().ToTable("FavoriteAds", "ads");
            modelBuilder.Entity<SaveAd>().ToTable("SaveAds", "ads");
            modelBuilder.Entity<PropertyContractPhoto>().ToTable("PropertyContractPhotos", "offers");

            //// offers schema
            modelBuilder.Entity<Offer>().ToTable("Offers", "offers");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoleConfig).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
