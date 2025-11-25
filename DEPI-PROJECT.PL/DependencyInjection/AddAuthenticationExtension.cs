using System.Security.Claims;
using System.Text;
using DEPI_PROJECT.DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace DEPI_PROJECT.PL.DependencyInjection
{
    public static class AddAuthenticationExtension
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddAuthentication(options =>
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecurityKey"]!)),
                    ClockSkew = TimeSpan.Zero
                };

                // Use JWT events for custom validation (modern approach)
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var principal = context.Principal;
                        var _userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
                        var user = _userManager.GetUserAsync(principal!).GetAwaiter().GetResult();

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

                        Claim TokenVersionClaim = userClaims.ToList().FirstOrDefault(c => c.Type == ClaimTypes.Version)!;

                        if (!principal!.HasClaim(c => c.Type == ClaimTypes.Version && c.Value == TokenVersionClaim!.Value))
                        {
                            // context.Fail()
                            context.Fail("Token version not matched");
                            return;
                        }
                    }
                };
            });
            return Services;
        }
    }
}