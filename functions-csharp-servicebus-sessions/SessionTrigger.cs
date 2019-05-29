using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Hollan.Function
{
    public class SessionTrigger
    {
        private readonly IOrderedListClient _client;
        public SessionTrigger(IOrderedListClient client) 
        {
            _client = client;
        }
        [FunctionName("SessionTrigger")]
        public async Task Run(
            [ServiceBusTrigger("vanilla", Connection = "ServiceBusConnectionString")]Message message, 
            ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {Encoding.UTF8.GetString(message.Body)}");
            await _client.PushData((string)message.UserProperties["patientId"], Encoding.UTF8.GetString(message.Body));
        }
    }
}
