using System.Collections.Generic;

namespace WebApplicationTraining.Models
{
    public class Supplier
    {
        public long SupplierId { get; set; } // pk
        public string Name { get; set; }
        public string City { get; set; }
        public IEnumerable<Product> Products { get; set; } // 1 -> molti
    }
}