using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.BLL.Services.Implements;
using DEPI_PROJECT.BLL.Mapper;
using DEPI_PROJECT.DAL.Repositories.Interfaces;
using DEPI_PROJECT.DAL.Repositories.Implements;

namespace DEPI_PROJECT.PL.DependencyInjection
{
    public static class AddServicesExtention
    {
        public static IServiceCollection AddServices(this IServiceCollection Services)
        {
            Services.AddScoped<IAuthService, AuthService>();
            Services.AddScoped<IJwtService, JwtService>();
            Services.AddScoped<IUserService, UserService>();
            Services.AddScoped<IRoleService, RoleService>();
            Services.AddScoped<IUserRoleService, UserRoleService>();
            Services.AddScoped<IPropertyService, PropertyService>();
            Services.AddScoped<IAgentService, AgentService>();
            Services.AddScoped<IBrokerService, BrokerService>();
            Services.AddScoped<IWishListService, WishListService>();
            Services.AddScoped<ICommentService, CommentService>();
            Services.AddScoped<ILikeCommentService, LikeCommentService>();
            Services.AddScoped<ILikePropertyService, LikePropertyService>();
            Services.AddScoped<IResidentialPropertyService, ResidentialPropertyService>();
            Services.AddScoped<ICommercialPropertyService, CommercialPropertyService>();
            Services.AddScoped<IPropertyGalleryService, PropertyGalleryService>();
            Services.AddScoped<ICompoundService, CompoundService>();
            
            return Services;
        }
    }
}
