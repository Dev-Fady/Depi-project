using DEPI_PROJECT.BLL.Services;
using DEPI_PROJECT.BLL.Services.Implements;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Repositories;
using DEPI_PROJECT.DAL.Repositories.Implements;
using DEPI_PROJECT.DAL.Repositories.Interfaces;

namespace DEPI_PROJECT.PL.DependencyInjection
{
    public static class PropertyGalleryServiceExtension
    {
        public static IServiceCollection AddPropertyGalleryServices(this IServiceCollection services)
        {
            services.AddScoped<IPropertyGalleryRepo, PropertyGalleryRepo>();
            services.AddScoped<IPropertyGalleryService, PropertyGalleryService>();
            return services;
        }
    }
}
