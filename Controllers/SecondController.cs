using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTraining.Controllers
{
    public class SecondController : Controller
    {
        public IActionResult Index()
        {
            return View("Common");
        }
    }
}