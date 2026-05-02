using Microsoft.AspNetCore.Mvc;
using StudentWeb.Models;

namespace StudentWeb.Controllers;

public class ResumeController : Controller
{
    public IActionResult Index()
    {
        return View(new ResumeViewModel());
    }
}
