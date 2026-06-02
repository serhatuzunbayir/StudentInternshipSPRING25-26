using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentWeb.Services;

namespace StudentWeb.Controllers;

[Authorize]
public class ResumeController : Controller
{
    private readonly ResumeBuilderService _resumeBuilderService;

    public ResumeController(ResumeBuilderService resumeBuilderService)
    {
        _resumeBuilderService = resumeBuilderService;
    }

    public IActionResult Index()
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var resume = _resumeBuilderService.BuildResume(userId);

        return View(resume);
    }

    public IActionResult Download()
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var fileBytes = _resumeBuilderService.GenerateResumeFile(userId);

        return File(fileBytes, "text/plain", "resume.txt");
    }
}