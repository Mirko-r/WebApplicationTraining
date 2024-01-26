using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTraining.Models;

namespace WebApplicationTraining.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        /*
         * 1. Asynchronous actions	
         * 2. Prevent over-binding
         * 3. Action results
         * 4. Redirection
         * 5. Data validation
         * 6. Omitting null properties
        */

        private DataContext _context;

        public ProductsController(DataContext ctx)
        {
            _context = ctx;
        }

        //public IEnumerable<Product> GetProducts()
        //{
        //    return _context.Products;
        //    //return new Product[] { new Product() { Name = "prd1" } };
        //}

        [HttpGet]
        public IAsyncEnumerable<Product> GetProducts() //it's bettere to use an async function
        {
            return (IAsyncEnumerable<Product>)_context.Products;
        }

        //public Product GetProduct(long id) // model-binding
        //{
        //    //return new Product[] { new Product() { Name = "prd2" } };
        //    //return _context.Products.FirstOrDefault();
        //    return _context.Products.Find(id);
        //}
        //public async Task<Product> GetProduct(long id) // using async - await
        //{
        //    return await _context.Products.FindAsync(id);
        //}
        //public async Task<IActionResult> GetProduct(long id) // using the correct status code
        //{
        //    Product p = await _context.Products.FindAsync(id);
        //    if (p == null)
        //    {
        //        return NotFound(); // 404
        //    }
        //    return Ok(p); // 200 (OK) + p as body
        //}

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetProduct(long id) // omitting null values
        {
            Product p = await _context.Products.FindAsync(id);
            if (p == null)
            {
                return NotFound(); // 404
            }
            return Ok(new
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                CategoryId = p.CategoryId,
                SupllierId = p.SupplierId
            }); // 200 (OK) + p as body
        }

        //public void SaveProduct ([FromBody]Product product) // model-binding on body of the request
        //{
        //    _context.Products.Add(product);
        //    _context.SaveChanges();
        //}
        //public async void SaveProduct([FromBody] Product product) // model-binding on body of the request
        //{
        //    await _context.Products.AddAsync(product);
        //    await _context.SaveChangesAsync();
        //}
        //public async Task SaveProduct([FromBody] ProductBindingTarget target)
        //{
        //    await _context.Products.AddAsync(target.ToProduct()); // it's better to use a binding target per eviatre l'overbinding
        //    await _context.SaveChangesAsync();
        //}
        //public async Task<IActionResult> SaveProduct([FromBody] ProductBindingTarget target) // using correct status code
        //{
        //    Product p = target.ToProduct();
        //    await _context.Products.AddAsync(p);
        //    await _context.SaveChangesAsync();

        //    return Ok(p); // 200 (OK) + p as body
        //}
        //public async Task<IActionResult> SaveProduct([FromBody] ProductBindingTarget target) // implementing data-validation
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Product p = target.ToProduct();
        //        await _context.Products.AddAsync(p);
        //        await _context.SaveChangesAsync();

        //        return Ok(p);
        //    }
        //    return BadRequest(ModelState); // 400
        //}

        [HttpPost]
        public async Task<IActionResult> SaveProduct([FromBody] ProductBindingTarget target)
        {
            // automatic validation due to apicontroller attribute
            Product p = target.ToProduct();
            await _context.Products.AddAsync(p);
            await _context.SaveChangesAsync();

            return Ok(p);
        }

        //public void UpdateProduct([FromBody]Product product)
        //{
        //    _context.Products.Update(product);
        //    _context.SaveChanges();
        //}
        //public async Task UpdateProduct([FromBody] Product product)
        //{
        //    _context.Products.Update(product);
        //    await _context.SaveChangesAsync();
        //}

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        //public void DeleteProduct(long id)
        //{
        //    _context.Products.Remove(new Product
        //    {
        //        ProductId = id
        //    });
        //    _context.SaveChanges();
        //}

        [HttpDelete("{id}")]
        public async Task DeleteProduct(long id)
        {
            _context.Products.Remove(new Product
            {
                ProductId = id
            });
            await _context.SaveChangesAsync();
        }

        //public IActionResult Redirect()
        //{
        //    return Redirect("/api/products/2"); // via url
        //}

        [HttpGet("redirect")]
        public IActionResult Redirect()
        {
            // anonymous object
            return RedirectToAction(nameof(GetProduct), new { Id = 2 }); // via name
        }
    }
}