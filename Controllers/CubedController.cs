using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace WebApplicationTraining.Controllers
{
    public class CubedController : Controller
    {
        public IActionResult Index()
        {
            return View("Cubed");
        }

        public IActionResult Cubed(int Num)
        {
            TempData["number"] = Num;
            TempData["cube"] = Math.Pow(Num, 3).ToString();
            return RedirectToAction(nameof(Index));
        }
    }
}
