using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
#nullable disable
    public class ForgotPasswordModel
    {
        [Required,EmailAddress,Display(Name ="Registered email address")]
        public string Email { get; set; }
        public bool EmailSend { get; set; }
    }
}
