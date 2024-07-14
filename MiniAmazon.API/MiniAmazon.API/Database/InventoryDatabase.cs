using MiniAmazon.Library.Models;

namespace MiniAmazon.API.Database
{
    public static class InventoryDatabase
    {
        public static List<Product> Products { get; } =
            new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Chair",
                    Description = "You sit on it",
                    Price = 19.99m,
                    Quantity = 3,
                    IsMarkdown = true,
                    MarkdownPrice = 12.99m
                },
                new Product
                {
                    Id = 2,
                    Name = "Eraser",
                    Description = "You erase with it",
                    Price = 4.99m,
                    Quantity = 7,
                    IsBOGO = true
                },
                new Product
                {
                    Id = 3,
                    Name = "Keyboard",
                    Description = "You can type with it",
                    Price = 79.99m,
                    Quantity = 6
                },
                new Product
                {
                    Id = 4,
                    Name = "Headphones",
                    Description = "You can listen to music with it",
                    Price = 39.99m,
                    Quantity = 3,
                    IsMarkdown = true,
                    MarkdownPrice = 31.99m
                }
            };
        public static int NextProductId
        {
            get
            {
                if (!Products.Any())
                    return 1;
                return Products.Select(p => p.Id).Max() + 1;
            }
        }
    }
}
