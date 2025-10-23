using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RealEstateBroker.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBroker.DAL.DataBase_Contexts
{
    public class RealEstateBrokerDbContext : DbContext
    {
        public RealEstateBrokerDbContext()
        {
        }

        public RealEstateBrokerDbContext(DbContextOptions<RealEstateBrokerDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Compound> Compounds { get; set; }
        public DbSet<PropertyGallery> PropertyGalleries { get; set; }
        public DbSet<CommercialProperty> CommercialProperties { get; set; }
        public DbSet<ResidentialProperty> ResidentialProperties { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=.;Database=RealEstateBrokerDB;Trusted_Connection=True;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Schema "accounts"
            modelBuilder.Entity<User>().ToTable("Users", schema: "accounts");
            modelBuilder.Entity<Broker>().ToTable("Brokers", schema: "accounts");
            modelBuilder.Entity<Agent>().ToTable("Agents", schema: "accounts");
            // Schema "properties"
            modelBuilder.Entity<Property>().ToTable("Properties", schema: "properties");
            modelBuilder.Entity<ResidentialProperty>().ToTable("ResidentialProperties", schema: "properties");
            modelBuilder.Entity<CommercialProperty>().ToTable("CommercialProperties", schema: "properties");
            modelBuilder.Entity<Amenity>().ToTable("Amenities", schema: "properties");
            modelBuilder.Entity<Compound>().ToTable("Compounds", schema: "properties");
            modelBuilder.Entity<PropertyGallery>().ToTable("PropertyGalleries", schema: "properties");

            // Schema "interactions"
            modelBuilder.Entity<Comment>().ToTable("Comments", schema: "interactions");
            modelBuilder.Entity<WishList>().ToTable("WishLists", schema: "interactions");


            base.OnModelCreating(modelBuilder);
        }
    }
}
