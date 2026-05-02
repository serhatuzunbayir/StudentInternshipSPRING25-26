using Microsoft.AspNetCore.Mvc;

namespace StudentWeb.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
