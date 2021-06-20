using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Vcillusion.Helloworld.V1;

namespace MessageServiceClient
{
    class Program
    {
        private const string MessageServiceHostKey = "MessageServiceHost";
        private const string MessageServiceHostDefault = "192.168.5.21";

        private const string MessageServicePortKey = "MessageServicePort";
        private const int MessageServicePortDefault = 30531;

        private static async Task Main()
        {
            Console.WriteLine("Welcome to Message Service Client!");
            string host = MessageServiceHostDefault;

            if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(MessageServiceHostKey)))
            {
                host = Environment.GetEnvironmentVariable(MessageServiceHostKey);
            }

            if (!int.TryParse(Environment.GetEnvironmentVariable(MessageServicePortKey), out var port))
            {
                port = MessageServicePortDefault;
            }

            Console.WriteLine($"Connecting to Message Service {host}:{port}!");

            var channel = new Channel(host, port, ChannelCredentials.Insecure);
            var client = new MessageStore.MessageStoreClient(channel);

            var index = 0;
            do
            {
                Console.WriteLine("Sending 1000 Messages...");
                var name = $"Message {index}";
                var response = await client.GetMessageAsync(new MessageRequest { Name = name }).ResponseAsync;
                Console.WriteLine($"Received: {response.Message}");
                Console.WriteLine("Waiting for 5 seconds...");
                index++;
                Thread.Sleep(5000);
            } while (index < 1000);

            channel.ShutdownAsync().Wait();
        }
    }
}
