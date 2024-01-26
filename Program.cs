using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApplicationTraining.Models;

namespace WebApplicationTraining
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // db population
            using (var context = new DataContext()) // call to .Dispose()
            {
                SeedData.SeedDatabase(context);
            }

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // default MvC approach
            builder.Services.AddControllersWithViews();

            //Razor pages (view+controller in one place)
            builder.Services.AddRazorPages();

            // custom map for pages
            builder.Services.Configure<RazorPagesOptions>(opts =>
                opts.Conventions.AddPageRoute("/index", "/extra/page/{id:long?}")
                ) ;

            // configuring object for dependency injection
            builder.Services.AddDbContext<DataContext>(opts =>
                opts.UseSqlServer(builder.Configuration["Sql:Connection:String"])
            );

            // JSON serializer configuration
            //builder.Services.Configure<JsonOptions>(opts =>
            //    opts.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            //);

            // replace default json serializer using newtonsoft  + XML serialization
            builder.Services.AddControllers().AddNewtonsoftJson().AddXmlSerializerFormatters();
            builder.Services.Configure<MvcNewtonsoftJsonOptions>(opts =>
                opts.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore);

            builder.Services.Configure<MvcOptions>(opts => // format 
             {
                 opts.RespectBrowserAcceptHeader = true; //negozazione
                 opts.ReturnHttpNotAcceptable = true; // status code 406

                 //custom messages fro conversion problems
                 opts.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                     value => "Please enter a value"
                     );
             });

            //activate swagger gen
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MyWebApi",
                    Version = "v1"
                });
            }
            );

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // JWT auth
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true, // chi ha rilasciato il token
                        ValidateAudience = true, // portatore del token
                        ValidateLifetime = true, // scadenza del token
                        ValidateIssuerSigningKey = true, // validare chiave che ha firmato il token
                        ValidIssuer = builder.Configuration["Jwt:Issuer"], // chi è l'issuer?
                        ValidAudience = builder.Configuration["Jwt:Audience"], // chi è l'audience
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) //chiave per firmare il token
                    };
                });

            //TempData -> "Sopravvive" in caso di redirect
            // Support complex Model in View without a class
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(opts =>
                opts.Cookie.IsEssential = true
            );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            //app.UseMiddleware<TestMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();//enabling razor pages

            app.UseSwagger();
            app.UseSwaggerUI(opt =>
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "MyWebApi")
            );

            app.Run();
        }
    }
}