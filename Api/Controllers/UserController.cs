using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChengetaWebApp.Api.Services;
using ChengetaWebApp.Api.Database.Models;

[Route("users")]
[ApiController]
[Authorize("Admin")]
public class UserController : ControllerBase
{
    private readonly DatabaseService _databaseService;
    public UserController(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [Route("/users")]
    public IActionResult GetUsers()
    {
        return Ok(_databaseService.GetAllUsers());
    }

    [HttpPost]
    [Authorize("Admin")]
    [ProducesResponseType(200)]
    [ProducesResponseType(422)]
    [Route("/users")]
    public async Task<IActionResult> AddUser([FromBody] GetUser user)
    {
        var result = await _databaseService.AddUser(user);
        if (result)
            return Ok();
        return UnprocessableEntity();
    }

    [HttpDelete]
    [Authorize("Admin")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [Route("/{guid}")]
    public async Task<IActionResult> DeleteUser(Guid guid)
    {
        var result = await _databaseService.DeleteUser(guid);
        if (result)
            return Ok();
        return NotFound();
    }

    [HttpPut]
    [Authorize("Admin")]
    [ProducesResponseType(200)]
    [ProducesResponseType(422)]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUser user)
    {
        var result = await _databaseService.UpdateUser(user);
        if (result)
            return Ok();
        return UnprocessableEntity();
    }
}