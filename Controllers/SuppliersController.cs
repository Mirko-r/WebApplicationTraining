using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplicationTraining.Models;

namespace WebApplicationTraining.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private DataContext _context;

        public SuppliersController(DataContext ctx)
        {
            _context = ctx;
        }

        [HttpGet("{id}")]
        public async Task<Supplier> GetSupplier(long id)
        {
            Supplier s = await _context.Suppliers
                .Include(s => s.Products)
                .FirstAsync(s => s.SupplierId == id);

            foreach(Product product in  s.Products)
            {
                product.Supplier = null;
            }

            return s;
        }

        [HttpPatch("{id}")] //Patch
        public async Task<Supplier> PatchSupplier(long id,[FromBody] JsonPatchDocument<Supplier> patchDoc)
        {
            Supplier s = await _context.Suppliers.FindAsync(id);
            if(s!=null)
            {
                patchDoc.ApplyTo(s);
                await _context.SaveChangesAsync();
            }
            return s;
        }
    }
}