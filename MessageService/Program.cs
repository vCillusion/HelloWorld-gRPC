using System;
using System.Threading;
using Grpc.Core;
using Vcillusion.Helloworld.V1;

namespace MessageService
{
    class Program
    {
        private const string MessageServicePortKey = "MessageServicePort";
        private const int MessageServicePortDefault = 5031;

        private static readonly AutoResetEvent Closing = new AutoResetEvent(false);

        static void Main()
        {
            Console.WriteLine("Welcome to Hello World Message Server!");
            const string host = "0.0.0.0";
            if (!int.TryParse(Environment.GetEnvironmentVariable(MessageServicePortKey), out var port))
            {
                port = MessageServicePortDefault;
            }

            var messageServiceServer = new Server()
            {
                Services = { MessageStore.BindService(new MessageServer()) },
                Ports = { new ServerPort(host, port, ServerCredentials.Insecure) }
            };
            messageServiceServer.Start();
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
