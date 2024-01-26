using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplicationTraining.Validation;

namespace WebApplicationTraining.Models
{
    [PhraseAndPrice(Phrase = "small", Price = "100")] // validation folder -> PhraseAndPriceAttribute
    public class Product
    {
        public long ProductId { get; set; }

        [Required(ErrorMessage = "Please enter a Name")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Please enter a Price")]
        [Column(TypeName = "decimal(8,2)")]
        [DisplayFormat(DataFormatString = "{0:c2}")]
        [Range(1, 99999, ErrorMessage = "Please enter a valid price")]
        public decimal Price { get; set; }

        [PrimaryKey(ContextType = typeof(DataContext), DataType = typeof(Category))] // validation folder -> PrimaryKeyAttribute
        [Remote("CategoryKey", "Validation", ErrorMessage = "Enter an existing category key")] // for jquery client-validation
        public long CategoryId { get; set; }
        public Category Category { get; set; }

        [PrimaryKey(ContextType =typeof(DataContext), DataType = typeof(Supplier))]
        [Remote("SupplierKey", "Validation", ErrorMessage = "Enter an existing  supplier key")] // for jquery client-validation

        public long SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
