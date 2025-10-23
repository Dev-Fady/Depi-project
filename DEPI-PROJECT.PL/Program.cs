using Microsoft.EntityFrameworkCore;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.BLL.Services.Implements;
namespace DEPI_PROJECT.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container
            builder.Services.AddControllers();

            // Add Entity Framework
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                                        b => b.MigrationsAssembly("DEPI-PROJECT.DAL")));

            builder.Services.AddIdentity<User, Role>()
                            .AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IJwtService, JwtService>();

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