namespace BookStore.Models
{
#nullable disable
    public class EmailConfirmModel
    {
        public string Email { get; set; }
        public bool IsConfirmed { get; set; }
        public bool SendEmail { get; set; }
        public bool EmailVerified { get; set; }
    }
}
