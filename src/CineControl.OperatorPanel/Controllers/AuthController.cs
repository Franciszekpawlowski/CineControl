using CineControl.OperatorPanel.Models.UserLogin;
using CineControl.OperatorPanel.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.OperatorPanel.Controllers
{
    [Route("[controller]/[action]")]
    public class AuthController(IAuthService authService) : Controller
    {
        private readonly IAuthService _authService = authService;

        public ActionResult Login()
        {
            if ( User.Identity.IsAuthenticated )
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserLoginRequest user)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Invalid username or password", "Invalid username or password");
                return View();
            }

            var result = await _authService.LoginAsync(user);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("Invalid username or password",result.Error.ToString());  
                return View();
            }

            return RedirectToAction("Index", "Cinema");
        }

        public async Task<ActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Login", "Auth");
        }
    }
}
