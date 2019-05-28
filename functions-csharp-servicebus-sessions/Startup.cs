using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

[assembly: FunctionsStartup(typeof(Hollan.Function.Startup))]

namespace Hollan.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IDatabase>((s) => {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(Environment.GetEnvironmentVariable("RedisDbConnectionString"));
                return redis.GetDatabase();
            });
            builder.Services.AddSingleton<IOrderedListClient, RedisOrderedListClient>();
        }
    }
}