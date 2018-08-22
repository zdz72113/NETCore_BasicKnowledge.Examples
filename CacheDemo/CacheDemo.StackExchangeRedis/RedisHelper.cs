using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CacheDemo.StackExchangeRedis
{
    public class RedisHelper
    {
        /// <summary>
        /// The options.
        /// </summary>
        private readonly RedisOptions _options;

        /// <summary>
        /// The connection multiplexer.
        /// </summary>
        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:EasyCaching.Redis.RedisDatabaseProvider"/> class.
        /// </summary>
        /// <param name="options">Options.</param>
        public RedisHelper(IOptions<RedisOptions> optionsAccessor)
        {
            if (optionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(optionsAccessor));
            }

            _options = optionsAccessor.Value;
            _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(CreateConnectionMultiplexer);
        }

        /// <summary>
        /// Gets the database connection.
        /// </summary>
        public IDatabase GetDatabase()
        {
            return _connectionMultiplexer.Value.GetDatabase();
        }

        /// <summary>
        /// Creates the connection multiplexer.
        /// </summary>
        /// <returns>The connection multiplexer.</returns>
        private ConnectionMultiplexer CreateConnectionMultiplexer()
        {
            if (_options.ConfigurationOptions != null)
            {
                return ConnectionMultiplexer.Connect(_options.ConfigurationOptions);
            }
            else
            {
                return ConnectionMultiplexer.Connect(_options.Configuration);
            }
        }
    }
}
