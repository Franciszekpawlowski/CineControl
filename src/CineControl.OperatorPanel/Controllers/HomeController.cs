using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CineControl.OperatorPanel.Models;
using Microsoft.AspNetCore.Authorization;

namespace CineControl.OperatorPanel.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
