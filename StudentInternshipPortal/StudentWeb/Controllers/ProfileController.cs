using Microsoft.AspNetCore.Mvc;
using StudentWeb.Models;

namespace StudentWeb.Controllers;

public class ProfileController : Controller
{
    public IActionResult Index()
    {
        return View(new ProfileEditViewModel());
    }
}
