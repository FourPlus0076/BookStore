using BookStore.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Repositories.Interface
{
    public interface IAccountRepository
    {
        Task<ApplicationUser> GetUserByEmailAsync(string email);

        Task<IdentityResult> CreateUser(SignUpUserModel model);
        Task<SignInResult> UserLogin(SignInUserModel model);
        Task SignOutUser();
        Task<IdentityResult> ChangePassword(ChangePasswordModel model);
        Task<IdentityResult> ConfirmEmailAsync(string uid, string token);

        Task GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task GenerateForgotPasswordAsyncTokenAsync(ApplicationUser user);

        Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model);
    }
}
