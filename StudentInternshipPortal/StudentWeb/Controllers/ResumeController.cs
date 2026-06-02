using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentWeb.Services;

namespace StudentWeb.Controllers;

// This controller allows students to preview and download their formatted resume as a text file.
[Authorize]
public class ResumeController : Controller
{
    private readonly ResumeBuilderService _resumeBuilderService;

    public ResumeController(ResumeBuilderService resumeBuilderService)
    {
        _resumeBuilderService = resumeBuilderService;
    }

    // Displays the formatted resume page in the web browser.
    public IActionResult Index()
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        string username = User.Identity?.Name ?? "";

        // Build the resume layout model
        var resume = _resumeBuilderService.BuildResume(userId, username);

        return View(resume);
    }

    // Handles downloading the student's resume as a clean text file (resume.txt).
    public IActionResult Download()
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        string username = User.Identity?.Name ?? "";

        // Generate CV text bytes in UTF8 format
        var fileBytes = _resumeBuilderService.GenerateResumeFile(userId, username);

        // Return file result with plain text MIME type
        return File(fileBytes, "text/plain", "resume.txt");
    }
}