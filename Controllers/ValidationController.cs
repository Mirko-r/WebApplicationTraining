using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApplicationTraining.Models;

namespace WebApplicationTraining.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValidationController : ControllerBase // for jquery client-validation
    {
        private DataContext _context;

        public ValidationController(DataContext ctx)
        {
            _context = ctx;
        }

        [HttpGet("categorykey")]
        public bool CategoryKey(string CategoryId)
        {
            return long.TryParse(CategoryId, out long keyval) && _context.Categories.Any(c => c.CategoryId == keyval);
        }

        [HttpGet("supplierkey")]
        public bool SupplierKey(string SupplierId)
        {
            return long.TryParse(SupplierId, out long keyval) && _context.Suppliers.Any(s => s.SupplierId == keyval);
        }
    }
}
