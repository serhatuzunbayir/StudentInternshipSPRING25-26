using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using StudentWeb.Services;

namespace StudentWeb.Controllers;

// This controller displays a student's notifications and updates their read statuses.
[Authorize]
public class NotificationsController : Controller
{
    private readonly NotificationQueryService _notificationQueryService;

    public NotificationsController(NotificationQueryService notificationQueryService)
    {
        _notificationQueryService = notificationQueryService;
    }

    // Displays the notifications page.
    public IActionResult Index()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var model = _notificationQueryService.GetNotificationsForUser(userId);
        return View(model);
    }

    // Marks a specific notification as read.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult MarkAsRead(int notificationId)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        _notificationQueryService.MarkAsRead(userId, notificationId);
        return RedirectToAction(nameof(Index));
    }

    // Marks all notifications of the student as read.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult MarkAllAsRead()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        _notificationQueryService.MarkAllAsRead(userId);
        return RedirectToAction(nameof(Index));
    }
}

