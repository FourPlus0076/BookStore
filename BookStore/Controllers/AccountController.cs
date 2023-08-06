using BookStore.Models;
using BookStore.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpUserModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.CreateUser(model);
                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(model);
                }
                ModelState.Clear();
                return RedirectToAction("ConfirmEmail", new {email = model.Email });
            }

            return View(model);
        }

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(SignInUserModel model, string returnURL)
        {
            if (ModelState.IsValid) {
                var result = await _accountRepository.UserLogin(model);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnURL))
                    {
                        return LocalRedirect(returnURL);
                    }
                    return RedirectToAction("Index", "Home");
                }
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Not allowed to login");
                }
                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Account Blocked try aftre some time");
                }
                else { ModelState.AddModelError("", "Invalid Credaincial"); }

            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _accountRepository.SignOutUser();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result= await _accountRepository.ChangePassword(model);
                if (result.Succeeded)
                {
                    ViewBag.IsSuccess=true;
                    ModelState.Clear();
                    return View();
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                }
            }
            return View();
        }
        [HttpGet("Confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string uid,string token,string email) 
        {
            EmailConfirmModel model = new EmailConfirmModel() { 
              Email = email,
            };
            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                token = token.Replace(' ','+');
                var result= await _accountRepository.ConfirmEmailAsync(uid,token);
                if (result.Succeeded)
                {
                   model.EmailVerified = true;
                }
            }
            return View(model);
        }

        [HttpPost("Confirm-email")]
        public async Task<IActionResult> ConfirmEmail(EmailConfirmModel model)
        {
           var user=await _accountRepository.GetUserByEmailAsync(model.Email);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    model.EmailVerified = true;
                    return View(model);
                }
                await _accountRepository.GenerateEmailConfirmationTokenAsync(user);
                model.SendEmail = true;
                ModelState.Clear();
            }
            else {
                ModelState.AddModelError("","Something went wrong");
            }
           return View(model);
        }
        [AllowAnonymous,HttpGet("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [AllowAnonymous, HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _accountRepository.GetUserByEmailAsync(model.Email);
                if (user != null)
                {
                    await _accountRepository.GenerateForgotPasswordAsyncTokenAsync(user);
                }
                ModelState.Clear();
                model.EmailSend = true;
            }
            return View(model);
        }
        [AllowAnonymous,HttpGet("reset-password")]
        public IActionResult ResetPassword(string uid,string token)
        {
            ResetPasswordModel model = new ResetPasswordModel()
            { 
               UserId= uid,
               Token=token
            };
            return View();
        }

        [AllowAnonymous,HttpPost("reset-password")]
        public async Task<IActionResult> ForgotPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                model.Token = model.Token.Replace(' ','+');
                var result =await _accountRepository.ResetPasswordAsync(model);
                if (result.Succeeded)
                {
                    ModelState.Clear();
                    model.IsSuccess = true;
                    return View(model);
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                }
              
            }
            return View(model);
        }
    }
}
