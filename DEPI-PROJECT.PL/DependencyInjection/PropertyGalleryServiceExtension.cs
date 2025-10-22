using DEPI_PROJECT.BLL.Manager.PropertyGallery;
using DEPI_PROJECT.DAL.Repository.PropertyGallery;

namespace DEPI_PROJECT.PL.DependencyInjection
{
    public static class PropertyGalleryServiceExtension
    {
        public static IServiceCollection AddPropertyGalleryServices(this IServiceCollection services)
        {
            services.AddScoped<IPropertyGalleryRepo, PropertyGalleryRepo>();
            services.AddScoped<IPropertyGalleryManager, PropertyGalleryManager>();
            return services;
        }
    }
}
