using System.Threading.Tasks;

namespace ServiceBus.Sender.Services
{
    public interface IMessagePublisher
    {
        Task SendMessageAsync<T>(T messagebject);
    }

}
