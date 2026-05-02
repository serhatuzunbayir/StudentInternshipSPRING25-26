using Microsoft.AspNetCore.Mvc;
using StudentWeb.Models;

namespace StudentWeb.Controllers;

public class JobsController : Controller
{
    public IActionResult Index()
    {
        return View(new JobSearchViewModel());
    }
}
