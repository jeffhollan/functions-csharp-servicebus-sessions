using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;

namespace session_queue_message_generator
{
    class Program
    {
        private static string connectionString = Environment.GetEnvironmentVariable("ServiceBusConnectionString");
        private static string queueName = Environment.GetEnvironmentVariable("QueueName");
        private static int sessions = 50;
        private static int messagePerSession = 10;
        static async Task Main(string[] args)
        {
            Console.WriteLine("Creating Service Bus sender....");
            var taskList = new List<Task>();
            var sender = new MessageSender(connectionString, queueName);

            for(int s = 0; s < sessions; s++)
            {
                var sessionId = s.ToString();
                var messageList = new List<Message>();
                for(int m = 0; m < messagePerSession; m++)
                {
                    var message = new Message(Encoding.UTF8.GetBytes($"Message-{m}")) 
                    {
                        SessionId = sessionId
                    };
                    messageList.Add(message);
                }
                taskList.Add(sender.SendAsync(messageList));
            }
            
            Console.WriteLine("Sending all messages...");
            await Task.WhenAll(taskList);
            Console.WriteLine("All messages sent.");
        }
    }
}
