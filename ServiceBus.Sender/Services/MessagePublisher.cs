using Microsoft.Azure.ServiceBus;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceBus.Sender.Services
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly ITopicClient _topicClient;

        public MessagePublisher(ITopicClient topicClient)
        {
            _topicClient = topicClient;
        }

        public async Task SendMessageAsync<T>(T messagebject)
        {
            var messageBody = JsonSerializer.Serialize(messagebject);

            var message = new Message(Encoding.UTF8.GetBytes(messageBody));

            message.UserProperties["messageType"] = typeof(T).Name;

            await _topicClient.SendAsync(message);
        }

    }

}
