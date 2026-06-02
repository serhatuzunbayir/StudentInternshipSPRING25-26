using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using StudentWeb.Models;
using StudentWeb.Services;
using System.IO;
using System.Linq;

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
    public IActionResult Submit(int jobId, string resumeOption, IFormFile? resumeFile)
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        string? resumeFileName = null;

        if (resumeOption == "upload")
        {
            if (resumeFile == null || resumeFile.Length == 0)
            {
                TempData["ApplicationMessage"] = "Please select a CV file to upload.";
                TempData["ApplicationMessageType"] = "error";
                return RedirectToAction("Index", "Jobs");
            }

            var allowedExtensions = new[] { ".pdf", ".doc", ".docx" };
            var fileExtension = Path.GetExtension(resumeFile.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                TempData["ApplicationMessage"] = "Invalid file type. Only PDF and Word documents (.pdf, .doc, .docx) are allowed.";
                TempData["ApplicationMessageType"] = "error";
                return RedirectToAction("Index", "Jobs");
            }

            if (resumeFile.Length > 5 * 1024 * 1024)
            {
                TempData["ApplicationMessage"] = "File is too large. Maximum size is 5MB.";
                TempData["ApplicationMessageType"] = "error";
                return RedirectToAction("Index", "Jobs");
            }

            try
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "resumes");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = $"Uploaded_CV_{userId}_{jobId}_{DateTime.UtcNow.Ticks}{fileExtension}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    resumeFile.CopyTo(fileStream);
                }

                resumeFileName = uniqueFileName;
            }
            catch (Exception ex)
            {
                TempData["ApplicationMessage"] = $"Failed to save uploaded file: {ex.Message}";
                TempData["ApplicationMessageType"] = "error";
                return RedirectToAction("Index", "Jobs");
            }
        }
        else if (resumeOption == "profile")
        {
            resumeFileName = "profile";
        }

        var result = _studentApplicationService.SubmitApplication(userId, jobId, resumeFileName);

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
