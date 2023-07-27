using BookStore.Models;
using BookStore.Repositories.Interface;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Repositories.Implementation
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        
        public AccountRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager= userManager;
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

    }
}
