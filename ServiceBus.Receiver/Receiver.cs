using Microsoft.Azure.ServiceBus;
using SharedModels;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBus.Receiver
{
    public static class Receiver
    {
        const string connentionString = "";
        const string topicName = "bahaa-topic";
        const string subscriptionName = "bahaa-subscription-a";

        static ISubscriptionClient _subscriptionClient;

        public static async Task ReceiveMessagesAsync()
        {
            _subscriptionClient = new SubscriptionClient(connentionString, topicName, subscriptionName);

            var messageHandlerOption = new MessageHandlerOptions(ExceptionHandler)
            {
                AutoComplete = false,
            };

            _subscriptionClient.RegisterMessageHandler(HandelMessageAsync, messageHandlerOption);

            Console.ReadLine();

            await _subscriptionClient.CloseAsync();
        }

        private static async Task HandelMessageAsync(Message message, CancellationToken token)
        {
            var messageType = message.UserProperties["messageType"].ToString();

            if (messageType == "EmployeeCreated")
            {
                var jsonString = Encoding.UTF8.GetString(message.Body);

                var employee = JsonSerializer.Deserialize<EmployeeCreated>(jsonString);

                Console.WriteLine($"Recived employee name : {employee.Name} and number : {employee.Number}");
            }
            else if (messageType == "CustomerCreated")
            {
                var jsonString = Encoding.UTF8.GetString(message.Body);

                var customer = JsonSerializer.Deserialize<CustomerCreated>(jsonString);

                Console.WriteLine($"Recived customer name : {customer.Name} and gender is : {customer.Gender}");
            }

            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private static Task ExceptionHandler(ExceptionReceivedEventArgs arg)
        {
            Console.WriteLine($"Message handler faild {arg.Exception.Message}");

            return Task.CompletedTask;
        }

    }
}
