using System;
using System.IO;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

[assembly: FunctionsStartup(typeof(Hollan.Function.Startup))]

namespace Hollan.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<ConnectionMultiplexer>((s) => {
                string configString = Environment.GetEnvironmentVariable("RedisDbConnectionString");
                var options = ConfigurationOptions.Parse(configString);
                options.ConnectTimeout = 40000;
                var cm = ConnectionMultiplexer.Connect(options);
                return cm;
            });
            builder.Services.AddScoped<IOrderedListClient, RedisOrderedListClient>();
        }
    }
}