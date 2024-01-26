using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationTraining.Models
{
    public class ProductBindingTarget
    {
        [Required]
        public string Name { get; set; }

        [Range(1,1000)]
        public decimal Price { get; set; }

        [Range(1, long.MaxValue)]
        public long CategoryId { get; set; }

        [Range(1, long.MaxValue)]
        public long SupplierId { get; set; }

        public Product ToProduct()
        {
            return new Product
            {
                Name = this.Name,
                Price = this.Price,
                CategoryId = this.CategoryId,
                SupplierId = this.SupplierId
            };
        }
    }
}
