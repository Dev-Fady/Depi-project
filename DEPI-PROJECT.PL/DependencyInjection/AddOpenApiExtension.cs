using Microsoft.OpenApi.Models;

namespace DEPI_PROJECT.PL.DependencyInjection
{
    public static class AddOpenApiExtension
    {
        public static IServiceCollection AddOpenApiSwagger(this IServiceCollection Services)
        {
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            Services.AddOpenApi();
            Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DEPI Real Estate API", Version = "v1" });

                // Add JWT Bearer Authentication to Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            return Services;
        }
    }
}