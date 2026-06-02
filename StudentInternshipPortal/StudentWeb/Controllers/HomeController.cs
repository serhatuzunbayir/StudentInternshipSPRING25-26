using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using StudentWeb.Models;
using StudentWeb.Services;

namespace StudentWeb.Controllers;

// This controller manages landing page views, loading dashboard stats for authenticated students.
public class HomeController : Controller
{
    private readonly JobBrowseService _jobBrowseService;
    private readonly StudentApplicationService _studentApplicationService;
    private readonly NotificationQueryService _notificationQueryService;

    public HomeController(
        JobBrowseService jobBrowseService,
        StudentApplicationService studentApplicationService,
        NotificationQueryService notificationQueryService)
    {
        _jobBrowseService = jobBrowseService;
        _studentApplicationService = studentApplicationService;
        _notificationQueryService = notificationQueryService;
    }

    // Displays the student home landing/dashboard page.
    public IActionResult Index()
    {
        // Check if the current user is authenticated
        var isAuthenticated = User.Identity?.IsAuthenticated == true;
        var model = new HomeViewModel
        {
            IsAuthenticated = isAuthenticated
        };

        // If logged in, fetch and load counts, notifications, and profile details
        if (isAuthenticated)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            model = _jobBrowseService.GetHomeViewModel(userId);
            model.TotalApplicationsCount = _studentApplicationService.GetApplicationCountForStudent(userId);
            model.TotalNotificationsCount = _notificationQueryService.GetNotificationCount(userId);
            model.UnreadNotificationsCount = _notificationQueryService.GetUnreadCount(userId);
        }

        return View(model);
    }
}

