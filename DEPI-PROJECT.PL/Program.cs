using AutoMapper;
using DEPI_PROJECT.BLL.Manager.ResidentialProperty;
using DEPI_PROJECT.BLL.Mapper;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Repository.ResidentialProperties;
using DEPI_PROJECT.PL.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
namespace DEPI_PROJECT.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Amenity amenity = new Amenity();
            // Add services to the container
            builder.Services.AddControllers();

            // Add Entity Framework
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddResidentialPropertyServices();
            builder.Services.AddPropertyGalleryServices();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapScalarApiReference(options =>
                {
                    options.Title = "The DEPI-REALESTATE Api";
                    options.Layout = ScalarLayout.Classic;
                    options.HideClientButton = true;
                    options.Theme = ScalarTheme.Saturn;
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.MapControllers();

            app.UseStaticFiles();
            app.Run();
        }
    }
}