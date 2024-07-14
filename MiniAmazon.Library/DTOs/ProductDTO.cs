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
    }
}
