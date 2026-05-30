using Microsoft.AspNetCore.Mvc;
using StudentWeb.Models;
using Microsoft.AspNetCore.Authorization;

namespace StudentWeb.Controllers;

[Authorize]
public class JobsController : Controller
{
    public IActionResult Index()
    {
        return View(new JobSearchViewModel());
    }
}
