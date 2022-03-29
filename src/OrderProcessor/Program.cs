using Azure.Messaging.ServiceBus;
using MyShop.Domain;
using System.Text.Json;

const string connectionString = "CONNECTION_STRING";
const string queue = "orders";

await using var client 
    = new ServiceBusClient(connectionString);

var receiver = client.CreateReceiver(
    queue,
    new ServiceBusReceiverOptions 
    { ReceiveMode = ServiceBusReceiveMode.PeekLock }
);

await foreach(var orderMessage in 
    receiver.ReceiveMessagesAsync())
{
    if (orderMessage.Body is null)
    {
        continue;
    }

    Order? order = null;

    try
    {
        order = 
            JsonSerializer.Deserialize<Order>(orderMessage.Body);
    }
    catch
    {
        await receiver.CompleteMessageAsync(orderMessage);
        continue;
    }

    if (order is not Order)
    {
        continue;
    }

    var items = order.Items.ToArray();

    Console.WriteLine($"Processing Order for {order.Customer} with {items.Length} items");

    foreach (var item in items)
    {
        Console.WriteLine($"\t\t{item.SKU} - {item.Price}");
    }

    await receiver.CompleteMessageAsync(orderMessage);

}