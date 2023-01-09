using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChengetaWebApp.Api.Services;
using ChengetaWebApp.Api.Database.Models;

[Route("notifications")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly DatabaseService _databaseService;
    public NotificationController(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    [HttpPost]
    [Authorize]
    public IResult UpdateNotification(string id, int status)
    {
        var res = _databaseService.UpdateNotificationStatus(Guid.Parse(id), status);
        return res;
    }

    /// <summary>
    /// Get all notifications with a limit
    /// </summary>
    [HttpGet]
    [Authorize]
    public IEnumerable<Notification> GetNotifications([FromQuery] GetNotificationsDto dto)
    {
        return _databaseService.GetAllNotifications(dto);
    }
}