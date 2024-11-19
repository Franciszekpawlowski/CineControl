using CineControl.OperatorPanel.Models;
using Microsoft.AspNetCore.Mvc;

namespace CineControl.OperatorPanel.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }
        }
    }
}
