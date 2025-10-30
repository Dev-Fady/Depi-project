using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.BLL.Services.Implements;
using DEPI_PROJECT.BLL.Mapper;
using DEPI_PROJECT.DAL.Repositories.Interfaces;
using DEPI_PROJECT.DAL.Repositories.Implements;

namespace DEPI_PROJECT.PL.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddResidentialPropertyServices(this IServiceCollection services)
        {
            // Add Services & Repositories
            services.AddScoped<IResidentialPropertyService, ResidentialPropertyService>();
            services.AddScoped<IResidentialPropertyRepo, ResidentialPropertyRepo>();

            // Add AutoMapper
            services.AddAutoMapper(typeof(ResidentialPropertyProfile));

            services.AddScoped<ICommercialPropertyService, CommercialPropertyService>();
            services.AddScoped<ICommercialPropertyRepo, CommercialPropertyRepo>();
           
            services.AddScoped<ICompoundRepo, CompoundRepo>();
            services.AddScoped<ICompoundService, CompoundService>();

            return services;
        }
    }
}
