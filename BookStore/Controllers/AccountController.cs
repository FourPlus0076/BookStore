using BookStore.Models;
using BookStore.Repositories.Interface;
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
            }

            return View();
        }

        [Route("Login")]
        public IActionResult UserLogin()
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
                else { ModelState.AddModelError("", "Invalid Credaincial"); }
                

            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _accountRepository.SignOutUser();
            return RedirectToAction("Index","Home");
        }
    }
}
