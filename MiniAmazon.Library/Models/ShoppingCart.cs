﻿namespace MiniAmazon.Library.Models
{
    public class ShoppingCart
    {
        public List<Product>? Items { get; set; }
        public ShoppingCart()
        {
            Items = new List<Product>();
        }
    }
}