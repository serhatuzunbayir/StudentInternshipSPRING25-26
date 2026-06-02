using Microsoft.AspNetCore.Mvc;
using StudentWeb.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using StudentWeb.Services;

namespace StudentWeb.Controllers;

// This controller allows students to search and browse job listings.
[Authorize]
public class JobsController : Controller
{
    private readonly JobBrowseService _jobBrowseService;

    public JobsController(JobBrowseService jobBrowseService)
    {
        _jobBrowseService = jobBrowseService;
    }

    // Handles displaying the job postings directory, applying filters passed via query strings.
    public IActionResult Index([FromQuery] JobSearchViewModel model)
    {
        // Parse current student ID and run the search query
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        model.Results = _jobBrowseService.SearchJobs(userId, model);
        return View(model);
    }
}

