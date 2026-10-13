using Microsoft.AspNetCore.Mvc;
using WebApplication1_07_10_2026.Models;

namespace WebApplication1_07_10_2026.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly string[] SampleNames = new[] { "Laptop", "Phone", "Headphones", "Monitor", "Keyboard" };
        private static readonly string[] Categories = new[] { "Electronics", "Accessories", "Peripherals" };

        [HttpGet(Name = "GetProducts")]
        public IEnumerable<Product> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Product
            {
                Id = index,
                Name = SampleNames[(index - 1) % SampleNames.Length],
                Category = Categories[Random.Shared.Next(Categories.Length)],
                Price = Math.Round((decimal)(Random.Shared.NextDouble() * 1000 + 20), 2),
                InStock = Random.Shared.Next(0, 2) == 1
            })
            .ToArray();
        }
    }
}
