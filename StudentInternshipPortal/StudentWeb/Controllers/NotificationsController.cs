using Microsoft.AspNetCore.Mvc;

namespace StudentWeb.Controllers;

public class NotificationsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
