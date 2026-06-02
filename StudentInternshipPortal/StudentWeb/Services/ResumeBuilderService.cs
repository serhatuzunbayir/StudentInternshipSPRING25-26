using System.Text;
using StudentWeb.Models;

namespace StudentWeb.Services;

public class ResumeBuilderService
{
    private readonly ProfileService _profileService;

    public ResumeBuilderService(ProfileService profileService)
    {
        _profileService = profileService;
    }

    public ResumeViewModel BuildResume(int userId)
    {
        var profile = _profileService.GetProfileByUserId(userId);

        if (profile == null)
        {
            return new ResumeViewModel
            {
                HasProfile = false,
                ResumeText = "No profile found. Please create your profile first."
            };
        }

        var resumeText =
            $@"RESUME
==============================

FULL NAME
{profile.FullName}

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

        return new ResumeViewModel
        {
            HasProfile = true,
            FullName = profile.FullName,
            AboutMe = profile.AboutMe,
            Skills = profile.Skills,
            Education = profile.Education,
            Experience = profile.Experience,
            Phone = profile.Phone,
            ResumeText = resumeText
        };
    }

    public byte[] GenerateResumeFile(int userId)
    {
        var resume = BuildResume(userId);

        return Encoding.UTF8.GetBytes(resume.ResumeText);
    }
}