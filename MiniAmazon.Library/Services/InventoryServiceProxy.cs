using MiniAmazon.Library.DTOs;
using MiniAmazon.Library.Models;
using MiniAmazon.Library.Utilities;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace MiniAmazon.Library.Services
{
    public class InventoryServiceProxy
    {
        private static InventoryServiceProxy? instance;

        // To Support Multi-Threading
        private static object instanceLock = new object();

        // Create A List of Products to Store In Inventory
        private List<ProductDTO> products;

        // Make Read Only
        public ReadOnlyCollection<ProductDTO>? Products
        {
            get { return products.AsReadOnly(); }
        }
        
        public async Task<IEnumerable<ProductDTO>> Get()
        {
            var result = await new WebRequestHandler().Get("/Inventory");
            var deserializedResult = JsonConvert.DeserializeObject<List<ProductDTO>>(result);
            products = deserializedResult?.ToList() ?? new List<ProductDTO>();
            return products;
        }

        public static InventoryServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new InventoryServiceProxy();
                    }
                }
                return instance;
            }
        }

        private InventoryServiceProxy()
        {
            var response = new WebRequestHandler().Get("/Inventory").Result;
            products = JsonConvert.DeserializeObject<List<ProductDTO>>(response);
        }
        
        public async Task<ProductDTO> AddorUpdate(ProductDTO p)
        {
            var response = await new WebRequestHandler().Post("/Inventory", p);
            return JsonConvert.DeserializeObject<ProductDTO>(response);
        }
        public void Remove(int id)
        {
            if (products == null)
                return;

            var productToDelete = products?.FirstOrDefault(c => c.Id == id);

            if (productToDelete != null)
                products?.Remove(productToDelete);
        }
    }
}