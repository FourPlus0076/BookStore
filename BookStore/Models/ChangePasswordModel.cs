using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BookStore.Models
{
#nullable disable
    public class ChangePasswordModel
    {
        [Required,DataType(DataType.Password),Display(Name ="Current Password")]
        public string OldPassword { get; set; }
        [Required,DataType(DataType.Password),Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [Required,DataType(DataType.Password),Display(Name = "Confirm Password")]
        [Compare("NewPassword",ErrorMessage = "Confirm new Password does not match")]
        public string ConfirmPassword { get; set;}

    }
}
