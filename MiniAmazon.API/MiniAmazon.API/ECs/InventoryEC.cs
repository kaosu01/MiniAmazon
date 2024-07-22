using MiniAmazon.API.Database;
using MiniAmazon.Library.DTOs;
using MiniAmazon.Library.Models;

namespace MiniAmazon.API.ECs
{
    public class InventoryEC
    {
        private IEnumerable<Product> Products { get; set; }

        public InventoryEC()
        {
            
        }

        public async Task<IEnumerable<ProductDTO>> Get()
        {
            return InventoryDatabase.Products.Take(100).Select(p => new ProductDTO(p));
        }

        public async Task<ProductDTO> AddorUpdate(ProductDTO p)
        {
            var isAdd = false;

            // If This is the First Time this Item is Being Added Into the Inventory
            if (p.Id == 0)
            {
                p.Id = InventoryDatabase.NextProductId;
                isAdd = true;
            }

            // We Need to Add the Product
            if (isAdd)
            {
                p.Name = p.Name?.ToLower();
                p.Name = char.ToUpper(p.Name[0]) + p.Name[1..];
                InventoryDatabase.Products.Add(new Product(p));
            }
            else
            {
                var prodToUpdate = InventoryDatabase.Products.FirstOrDefault(prod => prod.Id == p.Id);
                if (prodToUpdate != null)
                {
                    var index = InventoryDatabase.Products.IndexOf(prodToUpdate);
                    InventoryDatabase.Products.RemoveAt(index);
                    prodToUpdate = new Product(p);
                    InventoryDatabase.Products.Insert(index, prodToUpdate);
                }
            }

            /* FOR NOW LET'S ONLY UPDATE THE INVENTORY DATABASE NOT THE CARTS */
            //// Update the Product in Cart if Changes Were Made
            //var carts = ShoppingCartServiceProxy.Current.Carts;
            //for (int i = 0; i < carts.Count; i++)
            //{
            //    // Get Product In Cart if In Cart List
            //    var prod_In_Cart = carts[i].Items?.FirstOrDefault(cartItem => cartItem.Id == p.Id);

            //    if (prod_In_Cart != null)
            //    {

            //        prod_In_Cart.Name = p.Name;
            //        prod_In_Cart.Description = p.Description;
            //        prod_In_Cart.Price = p.Price;
            //        prod_In_Cart.IsMarkdown = p.IsMarkdown;
            //        prod_In_Cart.MarkdownPrice = p.MarkdownPrice;
            //        prod_In_Cart.IsBOGO = p.IsBOGO;
            //    }
            //}

            return p;
        }
    }
}
