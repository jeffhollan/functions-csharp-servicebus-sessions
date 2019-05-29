using System.Threading.Tasks;
using StackExchange.Redis;

namespace Hollan.Function
{
    public class RedisOrderedListClient : IOrderedListClient
    {
        private readonly ConnectionMultiplexer _redis;
        public RedisOrderedListClient(ConnectionMultiplexer redis) 
        {
            _redis = redis;
        }
        public async Task PushData(string key, string value)
        {
            var redisDb = _redis.GetDatabase();
            await redisDb.ListRightPushAsync(key, value);
        }
    }
}