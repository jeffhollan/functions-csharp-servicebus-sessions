using System.Threading.Tasks;
using StackExchange.Redis;

namespace Hollan.Function
{
    public class RedisOrderedListClient : IOrderedListClient
    {
        private readonly IDatabase _redisDb;
        public RedisOrderedListClient(IDatabase redisDb) 
        {
            _redisDb = redisDb;
        }
        public async Task PushData(string key, string value)
        {
            await _redisDb.ListRightPushAsync(key, value);
        }
    }
}