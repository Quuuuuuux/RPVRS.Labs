using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RPVRS.Labs.CacheDb.Common;

public static class RegistryExtensions
{
    public static IServiceCollection RegisterCacheDb(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<CacheDbOptions>()
            .Bind(configuration.GetSection(CacheDbOptions.ConfigName));

        services.AddSingleton<ICacheDb, CacheDb>();

        return services;
    }
}