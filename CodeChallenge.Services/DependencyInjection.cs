using CodeChallenge.DataAccess.Repositories;
using CodeChallenge.Services.Implementations;
using CodeChallenge.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CodeChallenge.Services
{
    /// <summary>
    /// Se agrega las dependencias de las clases
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>()
            .AddTransient<IProductService, ProductService>();

            services.AddTransient<IProductTypeRepository, ProductTypeRepository>()
            .AddTransient<IProductTypeService, ProductTypeService>();

            return services;
        }
    }
}
