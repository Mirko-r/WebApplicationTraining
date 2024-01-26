using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTraining.Models;

namespace WebApplicationTraining.Controllers
{
    public class HomeController : Controller
    {
        private DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        /*public async Task<IActionResult> Index(long id = 1)
        {
            Product prod = await _context.Products.FindAsync(id);

            // how to choose a view
            if(prod.CategoryId == 1)
            {
                return View("Watersports", prod);
            }
            else
            {
                return View(prod);
            }
        }*/

        public async Task<IActionResult> Index(long id = 2)
        {
            ViewBag.AveragePrice = await _context.Products.AverageAsync(p => p.Price);
            ViewBag.Info1 = "Hello!";

            Console.WriteLine($"************* {TempData["val"]}\n********{ViewBag.val}");

            return View(await _context.Products.FindAsync(id));
        }

        public IActionResult List()
        {
            return View(_context.Products);
        }

        public IActionResult Common()
        {
            return View();
        }

        public IActionResult M1() {
            TempData["val"] = "hellouueue";
            ViewBag.val = "viewag";
            return RedirectToAction(nameof(Index));
        }

    }
}