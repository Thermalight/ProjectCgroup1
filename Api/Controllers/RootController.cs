using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChengetaWebApp.Api.Services;

[Route("")]
[ApiController]
public class RootController : ControllerBase
{
    private readonly DatabaseService _databaseService;
    public RootController(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [Route("")]
    public ContentResult GetRoot()
    {
        return new ContentResult
        {
            Content = System.IO.File.ReadAllText(@"./wwwroot/index.html"),
            ContentType = "text/html"
        };
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [Route("/{**any}")]
    public ContentResult GetSpecified()
    {
        return new ContentResult
        {
            Content = System.IO.File.ReadAllText(@"./wwwroot/index.html"),
            ContentType = "text/html"
        };
    }
}