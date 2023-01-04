using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChengetaWebApp.Api.Services;

[Route("")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly DatabaseService _databaseService;
    public NotificationController(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [Route("/notifications")]
    public IActionResult GetNotifications()
    {
        return Ok(_databaseService.GetAllNotifications());
    }

    [HttpPost]
    [Authorize]
    [Route("/notifications")]
    public async Task<IResult> UpdateNotification(string id, int status)
    {
        var res = await _databaseService.UpdateNotificationStatusAsync(Guid.Parse(id), status);
        return res;
    }
}