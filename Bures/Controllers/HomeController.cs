using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bures.Models;
using Microsoft.AspNetCore.Authorization;

namespace Bures.Controllers;
// Controller for general site pages
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
// Keeping controllers lightweight improves testability and maintainability
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
// returns a plain view so content can be provided by the Razor page.
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Authorize]
    public IActionResult Character()
    {
        if (User.IsInRole("Admin"))
            return RedirectToAction("Index", "Choice", new { area = "Admin" });

        return View(); // Views/Home/Character.cshtml
    }

    [Authorize]
    public IActionResult NewUserPage()
    {
        // If admin, keep them on admin area
        if (User.IsInRole("Admin")) return RedirectToAction("Index", "Home", new { area = "Admin" });
        return View();
    }

    [Authorize]
    public IActionResult ContinuePage()
    {
        if (User.IsInRole("Admin")) return RedirectToAction("Index", "Home", new { area = "Admin" });
        return View();
    }
}
