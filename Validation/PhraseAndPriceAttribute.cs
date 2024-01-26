using System;
using System.ComponentModel.DataAnnotations;
using WebApplicationTraining.Models;

namespace WebApplicationTraining.Validation
{
    public class PhraseAndPriceAttribute : ValidationAttribute
    {
        public string Phrase { get; set; }
        public string Price { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is Product p)
            {
                if (p.Name.StartsWith(Phrase, StringComparison.OrdinalIgnoreCase) && p.Price > decimal.Parse(Price))
                    return new ValidationResult(ErrorMessage ?? $"Price cannot be more than {Price}$ for {Phrase} products");
                else
                    return ValidationResult.Success;
            }
            else
                throw new InvalidCastException($"{value.GetType} cannot be casted to {typeof(Product)}");
        }
    }
}
