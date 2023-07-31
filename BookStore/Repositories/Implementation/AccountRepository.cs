using BookStore.Models;
using BookStore.Repositories.Interface;
using BookStore.Service;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Repositories.Implementation
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }

        public async Task<IdentityResult> CreateUser(SignUpUserModel model)
        {
            var User = new ApplicationUser()
            {
                FName = model.FName,
                LName = model.LName,
                Email = model.Email,
                UserName = model.Email,
                DOB = DateTime.Now
            };
            var result = await _userManager.CreateAsync(User, model.Password);
            return result;
        }
        public async Task<SignInResult> UserLogin(SignInUserModel model)
        {
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

        }

        public async Task SignOutUser()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordModel model)
        {
            var userId = _userService.GetUserId();
            var user= await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user,model.OldPassword,model.NewPassword);
        }

    }
}
