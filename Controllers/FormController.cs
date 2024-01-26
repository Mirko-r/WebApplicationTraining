using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApplicationTraining.Models;

namespace WebApplicationTraining.Controllers
{
    public class FormController : Controller
    {
        public DataContext _context;
        public FormController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(long id = 2)
        {
            return View("Form", await _context.Products.FindAsync(id));
        }

        [HttpPost]
        public IActionResult SubmitForm(Product p) // Model-binding
        {
            //-> VALIDATION IS DONE ON THE MODEL
            //-> For the category and suplliers id
            //   validation is done in Validation -> PrimaryKeyAttribute
            //-> For combined logic -> Validaion -> PhraseAndPriceAttribute
            Console.WriteLine(p.Name + " " + p.Price + " " + p.CategoryId + " " + p.SupplierId);

            ////1. Manual validation 
            //if (string.IsNullOrEmpty(p.Name))
            //{
            //    ModelState.AddModelError(nameof(p.Name), "Please enter a name");
            //}
            //if (p.Price < 1)
            //{
            //    ModelState.AddModelError(nameof(p.Price), "Please enter a positive price");
            //}
            //if (!_context.Categories.Any(c => c.CategoryId == p.CategoryId))
            //{
            //    ModelState.AddModelError(nameof(p.CategoryId), "Please enter an existing category");
            //}
            //if (!_context.Suppliers.Any(s => s.SupplierId == p.SupplierId))
            //{
            //    ModelState.AddModelError(nameof(p.SupplierId), "Please enter an existing supplier");
            //}
            //logic validation (combined)
            //if (
            //    (ModelState.GetValidationState(nameof(p.Name)) == ModelValidationState.Valid) &&
            //    (ModelState.GetValidationState(nameof(p.Price)) == ModelValidationState.Valid) &&
            //     p.Name.StartsWith("small", StringComparison.OrdinalIgnoreCase) && p.Price > 100)
            //{
            //    ModelState.AddModelError("", "Small products cannot cost more than $100");
            //}


            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Results));
            }
            else
            {
                Console.WriteLine("Problems to solve");
                return View("Form");
            }
        }

        public IActionResult Results()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
