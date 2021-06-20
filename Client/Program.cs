using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Vcillusion.Helloworld.V1;

namespace Client
{
    internal class Program
    {
        private const string HelloWorldHostKey = "HelloWorldHost";
        private const string HelloWorldHostDefault = "192.168.5.21";// "localhost";

        private const string HelloWorldPortKey = "HelloWorldPort";
        private const int HelloWorldPortDefault = 30530;// 5030;
        private static async Task Main()
        {
            Console.WriteLine("Welcome to Hello World Client!");
            var host = HelloWorldHostDefault;

            if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(HelloWorldHostKey)))
            {
                host = Environment.GetEnvironmentVariable(HelloWorldHostKey);
            }

            if (!int.TryParse(Environment.GetEnvironmentVariable(HelloWorldPortKey), out var port))
            {
                port = HelloWorldPortDefault;
            }

            Console.WriteLine($"Connecting to Hello World {host}:{port}!");
            var channel = new Channel(host, port, ChannelCredentials.Insecure);
            var client = new Greeter.GreeterClient(channel);

            var index = 0;
            do
            {
                Console.WriteLine("Sending 1000 Messages...");
                var name = $"Message {index}";
                var response = await client.SayHelloAsync(new HelloRequest { Name = name }).ResponseAsync;
                Console.WriteLine($"Received: {response.Message}");
                Console.WriteLine("Waiting for 5 seconds...");
                index++;
                Thread.Sleep(5000);
            } while (index < 1000);

            channel.ShutdownAsync().Wait();
        }
    }
}
