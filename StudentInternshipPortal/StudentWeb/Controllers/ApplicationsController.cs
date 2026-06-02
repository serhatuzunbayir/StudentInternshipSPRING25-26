using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using StudentWeb.Models;
using StudentWeb.Services;
using System.IO;
using System.Linq;

namespace StudentWeb.Controllers;

// This controller handles viewing a student's applications list and submitting new job applications.
// [Authorize] ensures the student must be logged in to access these views.
[Authorize]
public class ApplicationsController : Controller
{
    private readonly StudentApplicationService _studentApplicationService;

    public ApplicationsController(StudentApplicationService studentApplicationService)
    {
        _studentApplicationService = studentApplicationService;
    }

    // Displays the list of applications submitted by the logged-in student.
    public IActionResult Index()
    {
        // Parse the logged-in student's user ID from claims identity
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var model = new ApplicationListViewModel
        {
            Applications = _studentApplicationService.GetApplicationsForStudent(userId)
        };

        return View(model);
    }

    // Handles the POST action when submitting a job application.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Submit(int jobId, string resumeOption, IFormFile? resumeFile)
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        string? resumeFileName = null;

        // If the student chooses to upload their own file
        if (resumeOption == "upload")
        {
            // Check if file is selected
            if (resumeFile == null || resumeFile.Length == 0)
            {
                TempData["ApplicationMessage"] = "Please select a CV file to upload.";
                TempData["ApplicationMessageType"] = "error";
                return RedirectToAction("Index", "Jobs");
            }

            // File type checks
            var allowedExtensions = new[] { ".pdf", ".doc", ".docx" };
            var fileExtension = Path.GetExtension(resumeFile.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
            {
                TempData["ApplicationMessage"] = "Invalid file type. Only PDF and Word documents (.pdf, .doc, .docx) are allowed.";
                TempData["ApplicationMessageType"] = "error";
                return RedirectToAction("Index", "Jobs");
            }

            // Size checks (limit to 5 MB)
            if (resumeFile.Length > 5 * 1024 * 1024)
            {
                TempData["ApplicationMessage"] = "File is too large. Maximum size is 5MB.";
                TempData["ApplicationMessageType"] = "error";
                return RedirectToAction("Index", "Jobs");
            }

            try
            {
                // Ensure the uploads directory exists on disk
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "resumes");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Generate a unique filename using user ID, job ID, and timestamp ticks
                var uniqueFileName = $"Uploaded_CV_{userId}_{jobId}_{DateTime.UtcNow.Ticks}{fileExtension}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save file content stream to disk
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
        // If the student chooses to submit their profile page details as CV
        else if (resumeOption == "profile")
        {
            resumeFileName = "profile";
        }

        // Submit application through the service
        var result = _studentApplicationService.SubmitApplication(userId, jobId, resumeFileName);

        // Display results to user via TempData messages
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

