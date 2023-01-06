using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ChengetaWebApp.Api.Services;
using ChengetaWebApp.Api.Services.MqttHandler;

if (File.Exists("./.env"))
{
    foreach (var line in File.ReadAllLines("./.env"))
    {
        var parts = line.Split('=', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 2)
            continue;

        Environment.SetEnvironmentVariable(parts[0], parts[1]);
    }
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<EmailService>();
builder.Services.AddSingleton<DatabaseService>();
builder.Services.AddSingleton<MqttService>();
builder.Services.AddAuthentication().AddCookie();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateAudience = true,
        ValidateIssuer = true,
        LifetimeValidator = (_, expires, _, _) => expires > DateTime.Now,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"]
    };
});

builder.Services.AddAuthorization(options =>
{
   options.AddPolicy("Admin", policy => policy.RequireClaim("admin", "True"));
});

var app = builder.Build();

app.Services.GetRequiredService<MqttService>().Start();
app.Services.GetRequiredService<DatabaseService>().Setup();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();