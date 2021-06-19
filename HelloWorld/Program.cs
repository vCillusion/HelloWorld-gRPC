using System;
using System.Threading;
using Grpc.Core;
using Vcillusion.Helloworld.V1;

namespace HelloWorld
{
    internal class Program
    {
        private const string MessageServiceHostKey = "MessageServiceHost";
        private const string MessageServiceHostDefault = "localhost";
        
        private const string MessageServicePortKey = "MessageServicePort";
        private const int MessageServicePortDefault = 5031;

        private const string HelloWorldPortKey = "HelloWorldPort";
        private const int HelloWorldPortDefault = 5030;
        
        private static readonly AutoResetEvent Closing = new AutoResetEvent(false);

        private static void Main()
        {
            Console.WriteLine("Welcome to Hello World Server!");
            const string host = "0.0.0.0";
            if (!int.TryParse(Environment.GetEnvironmentVariable(HelloWorldPortKey), out var port))
            {
                port = HelloWorldPortDefault;
            }

            Console.WriteLine("Starting the Message Service Client!");
            var messageServiceHost =
                Environment.GetEnvironmentVariable(MessageServiceHostKey) ?? MessageServiceHostDefault;
            
            if (!int.TryParse(Environment.GetEnvironmentVariable(MessageServicePortKey), out var messageServicePort))
            {
                messageServicePort = MessageServicePortDefault;
            }

            var channel = new Channel(messageServiceHost, messageServicePort, ChannelCredentials.Insecure);
            var client = new MessageStore.MessageStoreClient(channel);

            var helloWorldServer = new Server
            {
                Services = { Greeter.BindService(new HelloWorldServer(client)) },
                Ports = { new ServerPort(host, port, ServerCredentials.Insecure) }
            };
            helloWorldServer.Start();
            Console.WriteLine($"Listening on port {port}...");
            Console.CancelKeyPress += OnExit;
            Closing.WaitOne();
        }

        protected static void OnExit(object sender, ConsoleCancelEventArgs args)
        {
            Console.WriteLine("Exiting...");
            Closing.Set();
        }
    }
}
