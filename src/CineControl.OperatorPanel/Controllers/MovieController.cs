using Microsoft.AspNetCore.Mvc;

namespace CineControl.OperatorPanel.Controllers
{
    public class MovieController : Controller
    {
        // GET: MovieController
        public ActionResult Index()
        {
            return View();
        }

    }
}
