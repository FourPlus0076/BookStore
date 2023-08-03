using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
#nullable disable
    public class ResetPasswordModel
    {
        [Required]

        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }

        [Required,DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required,DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Password does not match")]
        public string ConfirmPassword { get; set; }

        public bool IsSuccess { get; set; }
    }
}
