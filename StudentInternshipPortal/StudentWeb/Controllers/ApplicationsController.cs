using Microsoft.AspNetCore.Mvc;

namespace StudentWeb.Controllers;

public class ApplicationsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
