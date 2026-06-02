using System.Text;
using StudentWeb.Models;

namespace StudentWeb.Services;

// This service is responsible for formatting raw student profile details into a clean text resume.
public class ResumeBuilderService
{
    private readonly ProfileService _profileService;

    public ResumeBuilderService(ProfileService profileService)
    {
        _profileService = profileService;
    }

    // Takes a student's database profile and formats it into a Resume view model.
    public ResumeViewModel BuildResume(int userId, string username)
    {
        // Load the profile details first
        var profile = _profileService.GetProfileByUserId(userId);

        // If student hasn't created a profile yet, return an alert text
        if (profile == null)
        {
            return new ResumeViewModel
            {
                HasProfile = false,
                ResumeText = "No profile found. Please create your profile first."
            };
        }

        // Format CV details into a readable text document layout
        var resumeText =
            $@"RESUME
==============================

FULL NAME
{profile.FullName}

EMAIL
{username}

PHONE
{profile.Phone}

ABOUT ME
{profile.AboutMe}

SKILLS
{profile.Skills}

EDUCATION
{profile.Education}

EXPERIENCE
{profile.Experience}
";

        // Package mapped values into the model
        return new ResumeViewModel
        {
            HasProfile = true,
            FullName = profile.FullName,
            Email = username,
            AboutMe = profile.AboutMe,
            Skills = profile.Skills,
            Education = profile.Education,
            Experience = profile.Experience,
            Phone = profile.Phone,
            ResumeText = resumeText
        };
    }

    // Converts the formatted resume text string into a raw UTF8 byte array so it can be downloaded.
    public byte[] GenerateResumeFile(int userId, string username)
    {
        var resume = BuildResume(userId, username);

        // Encode string text into a byte array
        return Encoding.UTF8.GetBytes(resume.ResumeText);
    }
}