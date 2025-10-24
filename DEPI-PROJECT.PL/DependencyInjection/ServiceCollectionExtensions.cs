using DEPI_PROJECT.BLL.Manager.CommercialProperty;
using DEPI_PROJECT.BLL.Manager.ResidentialProperty;
using DEPI_PROJECT.BLL.Mapper;
using DEPI_PROJECT.DAL.Repository.CommercialProperty;
using DEPI_PROJECT.DAL.Repository.ResidentialProperties;

namespace DEPI_PROJECT.PL.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddResidentialPropertyServices(this IServiceCollection services)
        {
            // Add Managers & Repositories
            services.AddScoped<IResidentialPropertyManager, ResidentialPropertyManager>();
            services.AddScoped<IResidentialPropertyRepo, ResidentialPropertyRepo>();

            // Add AutoMapper
            services.AddAutoMapper(typeof(ResidentialPropertyProfile));

            services.AddScoped<ICommercialPropertyManager, CommercialPropertyManager>();
            services.AddScoped<ICommercialPropertyRepo, CommercialPropertyRepo>();
            services.AddAutoMapper(typeof(CommercialPropertyProfile));
            return services;
        }
    }
}
