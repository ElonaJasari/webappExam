using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FirstMVC.Models;

namespace FirstMVC.Controllers;
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

    public IActionResult Character() // delete later and make an own controller
{
    return View();
}

}
