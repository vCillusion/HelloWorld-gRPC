using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Vcillusion.Helloworld.V1;

namespace MessageService
{
    class MessageServer : MessageStore.MessageStoreBase
    {
        public override Task<MessageResponse> GetMessage(MessageRequest request, ServerCallContext context)
        {
            var name = request.Name;
            Console.WriteLine($"Received request from {name}");

            const int millisecondsTimeout = 200;
            Thread.Sleep(millisecondsTimeout);
            var message = $"Hi {name}, this is an additional message from the Message Service (waited {millisecondsTimeout}Ms).";
            return Task.FromResult(new MessageResponse { Message = message });
        }
    }
}
