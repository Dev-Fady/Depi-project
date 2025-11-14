using DEPI_PROJECT.BLL.Constants;
using DEPI_PROJECT.BLL.Exceptions;
using DEPI_PROJECT.DAL.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace DEPI_PROJECT.PL.Helper_Function
{
    public static class GetAgentId
    {
        public static Guid GetAgentIdFromUserId(this ControllerBase controller, Guid UserId)
        {
            var claims = controller.User.Claims;
            var userIdClaim = claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier);
            var Role = claims.FirstOrDefault(a => a.Type == ClaimTypes.Role);

            if (Role == null)
            {
                throw new NotFoundException($"No role found with name {ClaimTypes.Role}");
            }

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                throw new InvalidOperationException("Authenticated User ID (NameIdentifier) not found or invalid.");
            }

            if (userId != UserId)
            {
                throw new UnauthorizedAccessException($"Unauthorized user, mismatch between current user {userId} and targeted user {UserId}");
            }

            if (!Enum.TryParse<UserRoleOptions>(Role.Value, true, out var role))
            {
                throw new UnauthorizedAccessException("Current user is not registered as agent, please register first");
            }

            try
            {
                return Guid.Parse(claims.FirstOrDefault(a => a.Type == ClaimsConstants.AGENT_ID).Value);
            }
            catch (ArgumentNullException ex)
            {
                throw new NotFoundException(ex.Message);
            }

        }
        public static Guid GetAgentIdFromClaims(this ControllerBase controller)
        {
            var claims = controller.User.Claims;
            var userIdClaim = claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier);
            var Role = claims.FirstOrDefault(a => a.Type == ClaimTypes.Role);
            
            if(Role == null)
            {
                throw new NotFoundException($"No role found with name {ClaimTypes.Role}");
            }

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                throw new InvalidOperationException("Authenticated User ID (NameIdentifier) not found or invalid.");
            }

            if (!Enum.TryParse<UserRoleOptions>(Role.Value, true, out var role))
            {
                throw new UnauthorizedAccessException("Current user is not registered as agent, please register first");
            }

            try
            {
                return Guid.Parse(claims.FirstOrDefault(a => a.Type == ClaimsConstants.AGENT_ID).Value);
            }
            catch (ArgumentNullException ex)
            {
                throw new NotFoundException(ex.Message);
            }

        }
    }
}
