using System.ComponentModel.DataAnnotations;

namespace BookStore.Helpers
{
#nullable disable
    public class CustomValidationAttributes : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,ValidationContext validationContext) 
        {
            if (value!=null)
            {
                string bookName = value.ToString();
                if (bookName.Contains("MVC")) 
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult ("Value is Empty");
        }
    }
}
