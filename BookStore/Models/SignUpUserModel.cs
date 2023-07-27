using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
#nullable disable
    public class SignUpUserModel
    {
        [Required(ErrorMessage ="Enter First Name")]
        [Display(Name ="First Name")]
        public string FName { get; set; }
        [Required(ErrorMessage = "Enter Last Name")]
        [Display(Name = "Last Name")]
        public string LName { get; set; }

        [Required(ErrorMessage ="Enter Email Address")]
        [Display(Name ="Email Address")]
        [EmailAddress(ErrorMessage ="Enter Valid Email")]        
        public string Email { get; set; }
        [Required(ErrorMessage ="Enter Storng Password")]
        [Display(Name ="Password")]
        [Compare("ConfirmPassword", ErrorMessage = "Password does not match")]
        [DataType(DataType.Password)]
       
        public string Password { get; set;}
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Enter Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set;}
    }
}
