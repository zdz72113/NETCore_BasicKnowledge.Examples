using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CacheDemo.StackExchangeRedis
{
    public static class RedisHelperServiceCollectionExtensions
    {
        public static IServiceCollection AddRedisHelper(this IServiceCollection services, Action<RedisOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            services.AddOptions();
            services.Configure(setupAction);
            services.AddSingleton<RedisHelper>();

            return services;
        }
    }
}
