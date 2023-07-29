using BookStore.Models;
using BookStore.Repositories.Interface;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Repositories.Implementation
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        
        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager= userManager;
            _signInManager= signInManager;
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
            var result=await _userManager.CreateAsync(User,model.Password);
            return result;
        }
        public async Task<SignInResult> UserLogin(SignInUserModel model)
        {
            return await _signInManager.PasswordSignInAsync(model.Email,model.Password,model.RememberMe,false);  
             
        }

        public async Task SignOutUser()
        {
            await _signInManager.SignOutAsync();
        }

    }
}
