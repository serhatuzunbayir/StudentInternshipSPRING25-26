using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using StudentWeb.Models;
using StudentWeb.Services;

namespace StudentWeb.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly ProfileService _profileService;

    public ProfileController(ProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var profile = _profileService.GetProfileByUserId(userId);

        if (profile == null)
            return View(new ProfileEditViewModel());

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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(ProfileEditViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        _profileService.UpsertProfile(new StudentProfile
        {
            UserId = userId,
            FullName = model.FullName,
            Skills = model.Skills,
            Education = model.Education,
            Experience = model.Experience,
            Phone = model.Phone,
            AboutMe = model.AboutMe
        });

        TempData["Success"] = "Profile updated successfully!";
        return RedirectToAction(nameof(Index));
    }
}