using System.Security.Claims;
using DEPI_PROJECT.BLL.Common;

namespace DEPI_PROJECT.PL.Middlewares
{
    public class SetAuthorizationContext
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<SetAuthorizationContext> _logger;

        public SetAuthorizationContext(RequestDelegate next, ILogger<SetAuthorizationContext> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {

                var UserId = context.User.FindFirst(a => a.Type == ClaimTypes.NameIdentifier);
                var Roles = context.User.FindAll(a => a.Type == ClaimTypes.Role).Select(c => c.Value);

                if (UserId == null)
                {
                    throw new UnauthorizedAccessException("No user Id found in the claims");
                }

                if (Guid.TryParse(UserId.Value, out var result))
                {
                    var authContext = new AuthorizationContext
                    {
                        UserId = result,
                        IsAdmin = Roles.Contains("ADMIN")
                    };
                    AuthorizationStore.Set(authContext);
                }
            }

            try
            {
                await _next(context);
            }
            finally
            {
                AuthorizationStore.Clear();
            }
        }
    }
}