using Microsoft.AspNetCore.Mvc;
using MiniAmazon.API.ECs;
using MiniAmazon.Library.DTOs;
using MiniAmazon.Library.Models;

namespace MiniAmazon.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController :ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IEnumerable<ProductDTO>> Get()
        {
            return await new InventoryEC().Get();
        }

        [HttpPost()]
        public async Task<ProductDTO> AddorUpdate([FromBody] ProductDTO p)
        {
            return await new InventoryEC().AddorUpdate(p);
        }
    }
}
