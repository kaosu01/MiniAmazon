namespace MiniAmazon.Library.Models
{
    public class ShoppingCart
    {
        int Id { get; set; }
        public List<Product>? Items { get; set; }
    }
}
