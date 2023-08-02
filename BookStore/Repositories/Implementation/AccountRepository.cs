using BookStore.Models;
using BookStore.Repositories.Interface;
using BookStore.Service.Interface;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Repositories.Implementation
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IUserService userService,IEmailService emailService,IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        { 
           return await _userManager.FindByEmailAsync(email);
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
            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(User);
                if (!string.IsNullOrEmpty(token))
                {
                    // await SendEmailConfirmationEmail(User, token);
                   await GenerateEmailConfirmationTokenAsync(User);
                }
            }
            return result;
        }

        public async Task GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendEmailConfirmationEmail(user, token);
            }           
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

        public async Task<IdentityResult> ConfirmEmailAsync(string uid,string token)
        {
           return await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid),token);
        }

        private async Task SendEmailConfirmationEmail(ApplicationUser user,string token)
        {
            var appDomain = _configuration.GetSection("Application:AppDomain").Value;
            var emailConfirmation = _configuration.GetSection("Application:EmailConfirmation").Value;

            UserEmailOptionsModel model = new UserEmailOptionsModel
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>() {
                      new KeyValuePair<string, string>("{{UserName}}",user.FName),
                      new KeyValuePair<string, string>("{{Link}}",string.Format(appDomain+emailConfirmation,user.Id,token))
                    },

            };
            await _emailService.SendEmailForEmailConfirmation(model);
        }

    }
}
