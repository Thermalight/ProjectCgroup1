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
    public IActionResult AddUser([FromBody] GetUser user)
    {
        // log all the user fields
        Console.WriteLine(user.Username);
        Console.WriteLine(user.Password);
        Console.WriteLine(user.Email);
        Console.WriteLine(user.IsAdmin);

        var result = _databaseService.AddUser(user);
        if (result)
            return Ok();
        return UnprocessableEntity();
    }

    [HttpDelete]
    [Authorize("Admin")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [Route("{guid}")]
    public IActionResult DeleteUser(string guid)
    {
        var result = _databaseService.DeleteUser(Guid.Parse(guid));
        if (result)
            return Ok();
        return NotFound();
    }

    [HttpPut]
    [Authorize("Admin")]
    [ProducesResponseType(200)]
    [ProducesResponseType(422)]
    public IActionResult UpdateUser([FromBody] UpdateUser user)
    {
        var result = _databaseService.UpdateUser(user);
        if (result)
            return Ok();
        return UnprocessableEntity();
    }
}