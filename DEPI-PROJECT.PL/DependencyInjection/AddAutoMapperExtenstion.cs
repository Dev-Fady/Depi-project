using DEPI_PROJECT.BLL.Mapper;

namespace DEPI_PROJECT.PL.DependencyInjection
{
    public static class AddAutoMapperExtension
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection Services)
        {
            Services.AddAutoMapper(typeof(ResidentialPropertyProfile));
            Services.AddAutoMapper(typeof(CommentProfile).Assembly);
            Services.AddAutoMapper(typeof(WishListProfile).Assembly);

            return Services;
        }
    }
}