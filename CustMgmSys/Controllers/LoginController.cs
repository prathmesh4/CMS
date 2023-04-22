using System.Threading.Tasks;
using CustMgmSys.Abstract;
using CustMgmSys.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace CustMgmSys.Controllers
{
    public class LoginController : Controller
    {

        //private readonly UserManager<UserModel> _userManager;
        //private readonly SignInManager<UserModel> _signInManager;

        //public LoginController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //}

        //[HttpGet]
        //public IActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Login(UserModel model)
        //{

        //    var user = await _userManager.FindByNameAsync(model.UserName);

        //    if (user != null)
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);

        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //    }

        //    ModelState.AddModelError("", "Invalid username or password");
        //    return View(model);
        //}

        //[HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Logout()
        //    {
        //        await _signInManager.SignOutAsync();
        //        return RedirectToAction("Login");
        //    }
        private readonly IUserLoginService _authService;
        public LoginController(IUserLoginService authService)
        {
            this._authService = authService;
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _authService.LoginAsync(model);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("Index", "Customers");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            if (!ModelState.IsValid) { return View(model); }
            model.Role = "user";
            var result = await this._authService.RegisterAsync(model);
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(Registration));
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this._authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }

    }
}
