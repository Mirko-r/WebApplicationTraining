using Microsoft.AspNetCore.Mvc;
using System;
using WebApplicationTraining.Models;

namespace WebApplicationTraining.Controllers
{
    public class InvoiceController : Controller
    {
        public IActionResult Index()
        {
            return View("Index", new Invoice());
        }

        [HttpPost]
        public IActionResult SubmitForm(Invoice i)
        {
            if (string.IsNullOrEmpty(i.Description))
            {
                ModelState.AddModelError(nameof(i.Description), "Please enter a description");
            }
            if(i.Amount <1 || i.Amount >100)
            {
                ModelState.AddModelError(nameof(i.Amount), "the amount shoul be >=1 and <=100");
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("Problems to solve");
                return View("Index");
            }
        }
    }
}