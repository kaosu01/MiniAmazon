using MiniAmazon.Library.DTOs;

namespace MiniAmazon.Library.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public List<ProductDTO>? Items { get; set; }

        public ShoppingCart()
        {
            Items = new List<ProductDTO>();
        }
    }
}
