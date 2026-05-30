using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace StudentWeb.Controllers;

[Authorize]
public class NotificationsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
