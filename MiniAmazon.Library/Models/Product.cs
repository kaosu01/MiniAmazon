namespace MiniAmazon.Library.Models
{
    public class Product
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
        public string Display() { return ToString(); }
        public Product() { }
        public override string ToString() { return $"({Id}) {Name}: {Description}\n    Price: ${Price}\tQuantity: {Count}\n"; }
    }
}