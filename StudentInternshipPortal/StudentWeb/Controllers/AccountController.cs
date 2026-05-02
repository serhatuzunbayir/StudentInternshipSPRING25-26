using Microsoft.AspNetCore.Mvc;
using StudentWeb.Models;

namespace StudentWeb.Controllers;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        return View(new LoginViewModel());
    }

    public IActionResult Register()
    {
        return View(new RegisterViewModel());
    }
}
