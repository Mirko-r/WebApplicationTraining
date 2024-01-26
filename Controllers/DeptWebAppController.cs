using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using System;
using WebApplicationTraining.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApplicationTraining.Controllers
{
    public class DeptWebAppController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                    );

                HttpResponseMessage response = await client.GetAsync("api/department");
                if (response.IsSuccessStatusCode)
                {
                    return View(await response.Content.ReadAsAsync<Department[]>());
                }
            }
            return View(null);
        }

        public async Task<IActionResult> Delete(long id)
        {
            using (var client = new HttpClient())
            {
                Console.WriteLine(id);
                client.BaseAddress = new Uri("https://localhost:7243/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                    );

                HttpResponseMessage response = await client.DeleteAsync($"api/department/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View("Error", new ErrorViewModel());
        }
    }
}
