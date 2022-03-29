using System;
using System.Linq;
using System.Text.Json;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using MyShop.Domain;

namespace OrderFunction
{
    public static class OrderFunction
    {
        [FunctionName("OrderFunction")]
        public static void Run([ServiceBusTrigger("orders", 
            Connection = "ServiceBusConnection")]string myQueueItem, 
            ILogger log)
        {
            var order = 
                JsonSerializer.Deserialize<Order>(myQueueItem);

            var items = order.Items.ToArray();

            Console.WriteLine($"Processing Order for {order.Customer} with {items.Length} items");

            foreach (var item in items)
            {
                Console.WriteLine($"\t\t{item.SKU} - {item.Price}");
            }

        }
    }
}
