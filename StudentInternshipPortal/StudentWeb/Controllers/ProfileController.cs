using Microsoft.AspNetCore.Mvc;
using StudentWeb.Models;
using Microsoft.AspNetCore.Authorization;

namespace StudentWeb.Controllers;

[Authorize]
public class ProfileController : Controller
{
    public IActionResult Index()
    {
        return View(new ProfileEditViewModel());
    }
}
