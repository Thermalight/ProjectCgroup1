using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChengetaWebApp.Api.Services;
using ChengetaWebApp.Api.Database.Models;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers;

[ApiController]
public class TokenController : ControllerBase
{
    public IConfiguration _configuration;
    private readonly DatabaseService _databaseService;

    public TokenController(IConfiguration config, DatabaseService databaseService)
    {
        _configuration = config;
        _databaseService = databaseService;
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Post(Credentials loginCredentials)
    {
        if (loginCredentials == null || loginCredentials.Email == null || loginCredentials.Password == null)
            return BadRequest();

        var user = _databaseService.VerifyUserLogin(loginCredentials.Email, loginCredentials.Password);

        if (user == null)
            return BadRequest("Invalid credentials");

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("id", user.GUID.ToString()),
            new Claim("name", user.Username),
            new Claim("email", user.Email),
            new Claim("admin", user.IsAdmin.ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims,
            expires: DateTime.Now.AddHours(24), signingCredentials: credentials);

        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
    }
}
