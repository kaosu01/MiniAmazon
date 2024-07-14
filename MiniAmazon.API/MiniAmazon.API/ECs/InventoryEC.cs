using MiniAmazon.API.Database;
using MiniAmazon.Library.Models;

namespace MiniAmazon.API.ECs
{
    public class InventoryEC
    {
        private IEnumerable<Product> Products { get; set; }

        public InventoryEC()
        {
            
        }

        public async Task<IEnumerable<Product>> Get()
        {
            return InventoryDatabase.Products.Take(100);
        }
    }
}
