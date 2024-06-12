namespace MiniAmazon.Library.Models
{
    public class Product
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
        public override string ToString() { return $"({Id}) {Name}: {Description}\n    Price: ${Price}\tQuantity: {Quantity}\n"; }
    }
}