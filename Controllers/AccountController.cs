using Microsoft.AspNetCore.Mvc;
using SistemasWeb01.Repository.Interfaces;
using SistemasWeb01.ViewModels;

namespace SistemasWeb01.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userRepository.LoginAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos.");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userRepository.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
    }

}

