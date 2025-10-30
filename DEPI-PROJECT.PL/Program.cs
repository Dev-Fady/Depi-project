using Microsoft.EntityFrameworkCore;
using DEPI_PROJECT.DAL.Models;
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


            // this line to read appsettings.Local.json and override settings normally read from appsettings.json
            builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true);
            // Add Entity Framework
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.MapControllers();
            
            app.Run();
        }
    }
}