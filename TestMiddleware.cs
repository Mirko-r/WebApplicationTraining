using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTraining.Models;

namespace WebApplicationTraining
{
    // http --> TestMiddleWare ---> next MiddleWare
    public class TestMiddleware
    {
        private RequestDelegate _requestDelegate;
        public TestMiddleware(RequestDelegate next)
        {
            _requestDelegate = next;
        }

        // C# reflection (introspezione di classe)
        public async Task Invoke(HttpContext context, DataContext dataContext)
        {
            Console.WriteLine("In custom MiddleWare");

            if (context.Request.Path.Equals("/test"))
            {
                await context.Response.WriteAsync($"There are {dataContext.Products.Count()} products");
            }
            else
            {
                await _requestDelegate(context);
            }
        }
    }
}