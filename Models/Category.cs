using System.Collections.Generic;

namespace WebApplicationTraining.Models
{
    public class Category
    {
        public long CategoryId { get; set; } // pk
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; } // 1 -> molti
    }
}