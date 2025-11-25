using DEPI_PROJECT.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DEPI_PROJECT.PL.Helper_Function
{
    public static class GetUserIdFromToken
    {
        public static Guid GetCurrentUserId(this ControllerBase controller)
        {
            var userIdClaim = controller.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return userId;
            }

            return Guid.Empty;
        }
    }
}
