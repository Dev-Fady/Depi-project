namespace DEPI_PROJECT.BLL.Common
{
    public static class CommonFunctions
    {
        public static bool CheckAuthorized(Guid GivenUser)
        {
            var AuthContext = AuthorizationStore.Current;

            if(AuthContext == null)
            {
                throw new UnauthorizedAccessException("No authorization context available");
            }

            return AuthContext.IsAdmin || AuthContext.UserId == GivenUser;
        }

        public static void EnsureAuthorized(Guid GivenUser)
        {
            if (!CheckAuthorized(GivenUser))
            {
                var AuthContext = AuthorizationStore.Current;
                throw new UnauthorizedAccessException(
                    $"Current user unauthorized to perform this action. Current ID: {GivenUser}, Target ID: {AuthContext.UserId}");
            }
        }
    }
}