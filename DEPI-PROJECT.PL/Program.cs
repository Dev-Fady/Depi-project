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

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecurityKey"]!)),
                    ClockSkew = TimeSpan.Zero
                };

                // Use JWT events for custom validation (modern approach)
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var principal = context.Principal;
                        var _userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
                        var user = _userManager.GetUserAsync(principal).GetAwaiter().GetResult();

                        if (user == null)
                        {
                            context.Fail("User not found");
                            return;
                        }

                        var userClaims = _userManager.GetClaimsAsync(user).GetAwaiter().GetResult();

                        if (userClaims.Count < 3)
                        {
                            context.Fail("Insufficient user claims");
                            return;
                        }

                        Claim TokenVersionClaim = userClaims.ToList().FirstOrDefault(c => c.Type == ClaimTypes.Version);

                        if (!principal.HasClaim(c => c.Type == ClaimTypes.Version && c.Value == TokenVersionClaim.Value))
                        {
                            // context.Fail()
                            context.Fail("Token version not matched");
                            return;
                        }
                    }
                };
            });

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IUserRoleService, UserRoleService>();
            builder.Services.AddScoped<IPropertyService, PropertyService>();
            builder.Services.AddScoped<IAgentService, AgentService>();
            builder.Services.AddScoped<IBrokerService, BrokerService>();


            builder.Services.AddScoped<IAgentRepo, AgentRepo>();
            builder.Services.AddScoped<IBrokerRepo, BrokerRepo>();



            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DEPI Real Estate API", Version = "v1" });

                // Add JWT Bearer Authentication to Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            // Register

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

            // Authentication & Authorization middleware (order is important!)
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.UseStaticFiles();
            app.Run();
        }

        
    }
}