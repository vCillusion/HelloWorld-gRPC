using System;
using System.Threading.Tasks;
using Grpc.Core;
using Vcillusion.Helloworld.V1;

namespace HelloWorld
{
    internal class HelloWorldServer: Greeter.GreeterBase
    {
        public override Task<HelloResponse> SayHello(HelloRequest request, ServerCallContext context)
        {
            var name = request.Name;
            Console.WriteLine($"Received request from {name}");
            var response = new HelloResponse
            {
                Message = $"Hello {name}, thank you for your message."
            };
            return Task.FromResult(response);
        }
    }
}
