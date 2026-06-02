using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using StudentWeb.Models;
using StudentWeb.Services;

namespace StudentWeb.Controllers;

// This controller allows students to view and update their profile details (resume, education, skills).
[Authorize]
public class ProfileController : Controller
{
    private readonly ProfileService _profileService;

    public ProfileController(ProfileService profileService)
    {
        _profileService = profileService;
    }

    // Displays the student's profile page containing their current inputs.
    [HttpGet]
    public IActionResult Index()
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // Fetch student profile using service
        var profile = _profileService.GetProfileByUserId(userId);

        if (profile == null)
            return View(new ProfileEditViewModel());

        // Map database model details to display view model
        return View(new ProfileEditViewModel
        {
            FullName = profile.FullName,
            Skills = profile.Skills,
            Education = profile.Education,
            Experience = profile.Experience,
            Phone = profile.Phone,
            AboutMe = profile.AboutMe
        });
    }

    // Saves or updates the student's profile information in the database.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(ProfileEditViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // Run upsert operation using service
        _profileService.UpsertProfile(new StudentProfile
        {
            UserId = userId,
            FullName = model.FullName,
            Skills = model.Skills ?? string.Empty,
            Education = model.Education ?? string.Empty,
            Experience = model.Experience ?? string.Empty,
            Phone = model.Phone ?? string.Empty,
            AboutMe = model.AboutMe ?? string.Empty
        });

        TempData["Success"] = "Profile updated successfully!";
        return RedirectToAction(nameof(Index));
    }
}