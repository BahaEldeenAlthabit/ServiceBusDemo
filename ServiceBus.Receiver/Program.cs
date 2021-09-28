using System.Threading.Tasks;

namespace ServiceBus.Receiver
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Receiver.ReceiveMessagesAsync();
        }
    }
}
