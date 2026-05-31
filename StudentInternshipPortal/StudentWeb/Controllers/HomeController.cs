using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using StudentWeb.Models;
using StudentWeb.Services;

namespace StudentWeb.Controllers;

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

    public IActionResult Index()
    {
        var isAuthenticated = User.Identity?.IsAuthenticated == true;
        var model = new HomeViewModel
        {
            IsAuthenticated = isAuthenticated
        };

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
