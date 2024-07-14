using MiniAmazon.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAmazon.Library.DTOs
{
    public class ProductDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal MarkdownPrice { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
        public bool IsMarkdown { get; set; }
        public bool IsBOGO { get; set; }

        public ProductDTO()
        {

        }

        public ProductDTO(Product p)
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
    }
}
