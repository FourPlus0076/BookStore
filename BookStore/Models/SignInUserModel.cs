using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
#nullable disable
    public class SignInUserModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
