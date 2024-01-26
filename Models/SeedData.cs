using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebApplicationTraining.Models
{// popolazione database
    public class SeedData
    {
        public static void SeedDatabase(DataContext context)
        {
            context.Database.Migrate(); // porto le classi dichiarate nel db creando le tabelle corrispondenti

            if(context.Users.Count() == 0 ) {
                context.Users.AddRange(
                    new User
                    {
                        Username = "fulvios",
                        Password = "123",
                        Role = "ADMIN , PUBLIC , DEV"
                    },
                    new User
                    {
                        Username = "sandras",
                        Password = "123",
                        Role = "PUBLIC"
                    }
                );

                context.SaveChanges();
            }

            // no-sql
            if(context.Products.Count() == 0 && 
                context.Suppliers.Count() == 0 && 
                context.Categories.Count() == 0)// non ci sono prodotti/supplier/categories
            {
                Supplier s1 = new Supplier
                {
                    Name = "supplier1",
                    City = "Turin"
                };
                Category c1 = new Category
                {
                    Name = "Watersports"
                };

                context.Products.AddRange( //genera l'Sql per aggiungere al db
                        new Product
                        {
                            Name = "Kayak",
                            Price = 275,
                            Category = c1,
                            Supplier = s1
                        },
                        new Product
                        {
                            Name = "Lifejacket",
                            Price = 48,
                            Category = c1,
                            Supplier = s1
                        }
                    );

                context.SaveChanges(); // esegue l'SQL (persistenza)
            }
        }
    }
}