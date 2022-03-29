namespace MyShop.Domain
{
    public record Order(string Customer, 
        IEnumerable<Item> Items); 
}