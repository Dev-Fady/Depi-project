using AutoMapper;
using DEPI_PROJECT.BLL.Mapper;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.PL.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.BLL.Services.Implements;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Security.Claims;
using DEPI_PROJECT.DAL.Repositories.Interfaces;
using DEPI_PROJECT.DAL.Repositories.Implements;
using System.Runtime.InteropServices;
using DEPI_PROJECT.PL.Middlewares;
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


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            //configure JSON options to serialize enums as strings
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
                });

            // Register Services
            builder.Services.AddOpenApiSwagger();
            builder.Services.AddAuthentication(builder.Configuration);
            builder.Services.AddAutoMapper();
            builder.Services.AddRepositores();
            builder.Services.AddServices();

            
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
            app.UseMiddleware<GlobalExceptionHandler>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAll");

            // Authentication & Authorization middleware (order is important!)
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.UseStaticFiles();
            app.Run();
        }
    }
}