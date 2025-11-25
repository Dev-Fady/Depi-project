namespace DEPI_PROJECT.BLL.Common
{
    public static class AuthorizationStore
    {
        private static readonly AsyncLocal<AuthorizationContext> _context = new();
        
        public static AuthorizationContext Current => _context.Value;
        
        public static void Set(AuthorizationContext context)
        {
            _context.Value = context;
        }
        
        public static void Clear()
        {
            _context.Value = null;
        }
    }
}