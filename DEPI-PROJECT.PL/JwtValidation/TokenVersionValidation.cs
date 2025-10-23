

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DEPI_PROJECT.DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace DEPI_PROJECT.PL.JwtValidation
{
    public class TokenVersionValidation : JwtBearerEvents
    {
        private readonly UserManager<User> _userManager;

        public TokenVersionValidation(
            UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public override async Task TokenValidated(TokenValidatedContext context)
        {
            var principal = context.Principal;
            // var _userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
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

            Claim TokenVersionClaim = userClaims.ToList().ElementAt(2);

            if (!principal.HasClaim(c => c.Type == ClaimTypes.Version && c.Value == TokenVersionClaim.Value))
            {
                // context.Fail()
                context.Fail("Token version not matched");
                return;
            }
        }
    }
}