using CineControl.OperatorPanel.Models.UserLogin;
using CineControl.OperatorPanel.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.OperatorPanel.Controllers
{
    public class AuthController(IAuthService authService) : Controller
    {
        private readonly IAuthService _authService = authService;

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserLoginRequest user)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }

            var result = await _authService.LoginAsync(user, HttpContext);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("",result.Error.ToString());  
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
