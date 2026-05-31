using Microsoft.AspNetCore.Mvc;
using StudentWeb.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using StudentWeb.Services;

namespace StudentWeb.Controllers;

[Authorize]
public class JobsController : Controller
{
    private readonly JobBrowseService _jobBrowseService;

    public JobsController(JobBrowseService jobBrowseService)
    {
        _jobBrowseService = jobBrowseService;
    }

    public IActionResult Index([FromQuery] JobSearchViewModel model)
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        model.Results = _jobBrowseService.SearchJobs(userId, model);
        return View(model);
    }
}
