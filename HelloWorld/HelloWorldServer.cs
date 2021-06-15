using System;
using System.Threading.Tasks;
using Grpc.Core;
using Vcillusion.Helloworld.V1;

namespace HelloWorld
{
    internal class HelloWorldServer: Greeter.GreeterBase
    {
        private readonly MessageStore.MessageStoreClient _client;

        public HelloWorldServer(MessageStore.MessageStoreClient client)
        {
            _client = client;
        }
        public override async Task<HelloResponse> SayHello(HelloRequest request, ServerCallContext context)
        {
            var name = request.Name;
            Console.WriteLine($"Received request from {name}");

            var messageFromStore = await _client.GetMessageAsync(new MessageRequest {Name = request.Name});
            var response = new HelloResponse
            {
                Message = $"Hello {name}, thank you for your message. Store: {messageFromStore}"
            };
            return await Task.FromResult(response);
        }
    }
}
