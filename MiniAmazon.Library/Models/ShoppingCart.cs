namespace MiniAmazon.Library.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public List<Product>? Items { get; set; }
    }
}
