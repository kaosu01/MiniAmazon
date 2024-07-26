using MiniAmazon.Library.DTOs;
using MiniAmazon.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniAmazon.MAUI.ViewModels
{
    // Made By Professor Mills
    internal class ImportViewModel
    {
        public string FilePath { get; set; }

        public async void ImportFile(Stream? stream = null)
        {
            StreamReader? sr = null;
            try
            {
                if (stream == null)
                    sr = new StreamReader(FilePath);
                else
                    sr = new StreamReader(stream);
            }
            catch (Exception ex) { }

            string line = string.Empty;
            var products = new List<ProductDTO>();
            while ((line = sr.ReadLine()) != null)
            {
                var tokens = line.Split(['|']);

                products.Add(new ProductDTO
                {
                    Name = tokens[0],
                    Description = tokens[1],
                    Price = decimal.Parse(tokens[2]),
                    MarkdownPrice = decimal.Parse(tokens[3]),
                    Quantity = int.Parse(tokens[4]),
                    IsMarkdown = bool.Parse(tokens[5]),
                    IsBOGO = bool.Parse(tokens[6])
                });
            }

            foreach (var product in products)
            {
                await InventoryServiceProxy.Current.AddorUpdate(product);
            }

            await Shell.Current.GoToAsync("//Inventory");
        }
    }
}
