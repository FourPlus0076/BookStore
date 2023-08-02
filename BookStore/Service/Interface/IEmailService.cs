using BookStore.Models;

namespace BookStore.Service.Interface
{
    public interface IEmailService
    {     
        Task SendTestEmail(UserEmailOptionsModel model);
        Task SendEmailForEmailConfirmation(UserEmailOptionsModel model);
    }
}