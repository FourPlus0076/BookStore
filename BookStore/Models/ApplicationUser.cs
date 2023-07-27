using Microsoft.AspNetCore.Identity;

namespace BookStore.Models
{
#nullable disable
    public class ApplicationUser : IdentityUser
    {
        public string FName { get; set; }
        public string LName { get; set;}
        public DateTime DOB { get; set;}
    }
}
