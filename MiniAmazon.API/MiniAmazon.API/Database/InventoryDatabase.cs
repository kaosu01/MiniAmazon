using MiniAmazon.Library.Models;

namespace MiniAmazon.API.Database
{
    public static class InventoryDatabase
    {
        public static IEnumerable<Product> Products { get; } =
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
                }
            };
    }
}
