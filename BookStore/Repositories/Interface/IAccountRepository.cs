using BookStore.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Repositories.Interface
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUser(SignUpUserModel model);
        Task<SignInResult> UserLogin(SignInUserModel model);
    }
}
