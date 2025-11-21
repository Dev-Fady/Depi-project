namespace DEPI_PROJECT.BLL.Common
{
    public class AuthorizationContext
    {
        public Guid UserId { get; set; }
        public bool IsAdmin { get; set; }
    }
}