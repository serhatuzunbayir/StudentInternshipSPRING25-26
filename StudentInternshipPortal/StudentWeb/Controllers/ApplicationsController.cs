using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using StudentWeb.Models;
using StudentWeb.Services;

namespace StudentWeb.Controllers;

[Authorize]
public class ApplicationsController : Controller
{
    private readonly StudentApplicationService _studentApplicationService;

    public ApplicationsController(StudentApplicationService studentApplicationService)
    {
        _studentApplicationService = studentApplicationService;
    }

    public IActionResult Index()
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var model = new ApplicationListViewModel
        {
            Applications = _studentApplicationService.GetApplicationsForStudent(userId)
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Submit(int jobId)
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = _studentApplicationService.SubmitApplication(userId, jobId);

        TempData["ApplicationMessage"] = result switch
        {
            SubmitApplicationResult.Success => "Your application was submitted successfully.",
            SubmitApplicationResult.AlreadyApplied => "You have already applied for this job.",
            SubmitApplicationResult.JobUnavailable => "This job is no longer available for applications.",
            SubmitApplicationResult.ProfileMissing => "Complete your student profile before applying.",
            _ => "Application submission failed."
        };

        TempData["ApplicationMessageType"] = result == SubmitApplicationResult.Success ? "success" : "error";

        return RedirectToAction("Index", "Jobs");
    }
}
