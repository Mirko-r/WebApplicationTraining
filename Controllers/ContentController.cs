using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTraining.Models;

namespace WebApplicationTraining.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ContentController : ControllerBase
    {
        private DataContext _context;

        public ContentController(DataContext ctx)
        {
            _context = ctx;
        }

        [HttpGet("string")] //return text/plain as default NOT JSON
        public string GetString() => "string";

        [HttpGet("object/{format?}")]// the requested fromat is specified usign the url
        [FormatFilter]
        [Produces("application/json", "application/xml")]
        public async Task<ProductBindingTarget> GetObject()
        {
            Product p = await _context.Products.FirstAsync();
            return new ProductBindingTarget { Name = p.Name, CategoryId = p.CategoryId, Price = p.Price, SupplierId = p.SupplierId };
        }
    }
}
