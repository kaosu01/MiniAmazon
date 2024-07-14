using MiniAmazon.Library.DTOs;

namespace MiniAmazon.Library.Models
{
    public class Product
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal MarkdownPrice { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
        public bool IsMarkdown { get; set; }
        public bool IsBOGO { get; set; }

        public string? Display() { return ToString();}
        public override string ToString() { return $"({Id}) {Name}: {Description}\n    Price: ${Price}\tQuantity: {Quantity}\n"; }

        public Product(ProductDTO p)
        {
            Name = p.Name;
            Description = p.Description;
            Price = p.Price;
            MarkdownPrice = p.MarkdownPrice;
            Id = p.Id;
            Quantity = p.Quantity;
            IsMarkdown = p.IsMarkdown;
            IsBOGO = p.IsBOGO;
        }

        public Product()
        {

        }
    }
}