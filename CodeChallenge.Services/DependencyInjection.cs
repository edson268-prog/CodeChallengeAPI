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
            //TODO: Why Transient.!! :( 
            //ANSWER: Changed to Scoped to a best performance
            services.AddScoped<IProductRepository, ProductRepository>()
            .AddTransient<IProductService, ProductService>();

            services.AddScoped<IProductTypeRepository, ProductTypeRepository>()
            .AddTransient<IProductTypeService, ProductTypeService>();

            return services;
        }
    }
}
