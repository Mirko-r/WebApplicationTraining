using Microsoft.EntityFrameworkCore;
using System;

namespace WebApplicationTraining.Models
{
    // Indica quali Model (tabelle) voglio effettivamente "Trasportare" sul db 
    // Collegamento al db
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opts) : base(opts){ }

        public DataContext() { }

        // objects to persist over a db
        public DbSet<Product> Products { get; set; } // le instanze potranno essere salvate come record
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Console.WriteLine("in Onconfiguring");
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=MyWebAppDB;Persist Security Info=True;User ID=sa;Password=Uform@2023#;TrustServerCertificate=True");
        }
    }
}