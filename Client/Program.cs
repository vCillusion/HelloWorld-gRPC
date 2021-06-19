using System;
using System.Threading.Tasks;
using Grpc.Core;
using Vcillusion.Helloworld.V1;

namespace Client
{
    internal class Program
    {
        private static async Task Main()
        {
            Console.WriteLine("Welcome to Hello World Client!");
            const string host = "localhost";
            const int port = 5030;
            var channel = new Channel(host, port, ChannelCredentials.Insecure);
            var client = new Greeter.GreeterClient(channel);
            do
            {
                Console.WriteLine("Please enter your name...");
                var name = Console.ReadLine();
                var response = await client.SayHelloAsync(new HelloRequest { Name = name }).ResponseAsync;
                Console.WriteLine($"Received: {response.Message}");
                Console.WriteLine("Do you wanted to send more messages? (y/n)...");
            } while (Console.ReadLine() == "y");
            channel.ShutdownAsync().Wait();
        }
    }
}
