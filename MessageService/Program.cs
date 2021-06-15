using System;
using System.Threading;
using Grpc.Core;
using Vcillusion.Helloworld.V1;

namespace MessageService
{
    class Program
    {
        private const string MessageServicePortKey = "MessageServicePort";
        private const int MessageServicePortDefault = 3031;

        private static readonly AutoResetEvent Closing = new AutoResetEvent(false);

        static void Main()
        {
            Console.WriteLine("Welcome to Hello World Message Server!");
            const string host = "localhost";
            if (!int.TryParse(Environment.GetEnvironmentVariable(MessageServicePortKey), out var port))
            {
                port = MessageServicePortDefault;
            }
            var helloWorldServer = new Server
            {
                Services = { MessageStore.BindService(new MessageServer()) },
                Ports = { new ServerPort(host, port, ServerCredentials.Insecure) }
            };
            helloWorldServer.Start();
            Console.WriteLine("Listening...");
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
