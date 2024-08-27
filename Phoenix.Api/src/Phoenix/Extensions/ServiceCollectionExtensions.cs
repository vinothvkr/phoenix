using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Phoenix.Conventions;
using Phoenix.Providers;
using System.Reflection;

namespace Phoenix.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration, Assembly applicationServiceAssembly)
    {
        services.AddControllers(o => o.Conventions.Add(
                new PhoenixAppServiceConvention()
            ))
            .AddApplicationPart(applicationServiceAssembly)
            .ConfigureApplicationPartManager(manager =>
            {
                manager.FeatureProviders.Add(new PhoenixControllerFeatureProvider());
            })
            .AddControllersAsServices();
        return services;
    }
}
