using System;
using Grpc.Core;
using Vcillusion.Helloworld.V1;

namespace HelloWorld
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Welcome to Hello World Server!");
            const string host = "localhost";
            const int port = 3030;
            var helloWorldServer = new Server
            {
                Services = { Greeter.BindService(new HelloWorldServer()) },
                Ports = { new ServerPort(host, port, ServerCredentials.Insecure) }
            };
            helloWorldServer.Start();
            Console.WriteLine("Listening...");
            Console.WriteLine("Please enter key to exit...");
            Console.ReadLine();
        }
    }
}
