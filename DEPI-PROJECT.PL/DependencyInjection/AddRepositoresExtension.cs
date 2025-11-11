using DEPI_PROJECT.BLL.Services;
using DEPI_PROJECT.BLL.Services.Implements;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Repositories;
using DEPI_PROJECT.DAL.Repositories.Implements;
using DEPI_PROJECT.DAL.Repositories.Interfaces;

namespace DEPI_PROJECT.PL.DependencyInjection
{
    public static class AddRepositoresExtension
    {
        public static IServiceCollection AddRepositores(this IServiceCollection Services)
        {
            Services.AddScoped<IAgentRepo, AgentRepo>();
            Services.AddScoped<IBrokerRepo, BrokerRepo>();
            Services.AddScoped<IWishListRepository , WishListRepository>();
            Services.AddScoped<ICommentRepository, CommentRepository>();
            Services.AddScoped<ILikeCommentRepo, LikeCommentRepo>();
            Services.AddScoped<ILikePropertyRepo, LikePropertyRepo>();
            Services.AddScoped<IPropertyGalleryRepo, PropertyGalleryRepo>();
            Services.AddScoped<IPropertyGalleryService, PropertyGalleryService>();
            Services.AddScoped<IResidentialPropertyRepo, ResidentialPropertyRepo>();
            Services.AddScoped<ICommercialPropertyRepo, CommercialPropertyRepo>();
            Services.AddScoped<ICompoundRepo, CompoundRepo>();

            return Services;
        }
    }
}
