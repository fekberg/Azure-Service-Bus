// using Azure.Messaging.ServiceBus;
// using MyShop.Domain;
using Azure.Messaging.ServiceBus;
using MyShop.Domain;
using MyStore.Pages;
using System.Text.Json;

namespace MyStore.Integration
{
    public class OrderService
    {
        #region Connection String
        private const string connectionString = "CONNECTION_STRING";
        private const string queue = "orders";
        #endregion

        public async Task QueueOrder(Order order)
        {
            await using var client =
                new ServiceBusClient(connectionString);

            var sender = client.CreateSender(queue);

            await sender.SendMessageAsync(
                new(JsonSerializer.SerializeToUtf8Bytes(order))
            );
        }
    }
}
