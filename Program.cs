using ChengetaWebApp.Api.Services;
using ChengetaWebApp.Api.Services.MqttHandler;
using System.Text.Json;

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

builder.Services.AddSingleton<DatabaseService>();
builder.Services.AddSingleton<MqttService>();

var app = builder.Build();

app.Services.GetRequiredService<MqttService>().Start();
app.Services.GetRequiredService<DatabaseService>().Setup();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    var _databaseService = app.Services.GetRequiredService<DatabaseService>();

    endpoints.MapGet("/", async context =>
    {
        context.Response.Headers.Add("Content-Type", "text/html");
        await context.Response.WriteAsync(System.IO.File.ReadAllText(@"./wwwroot/index.html"));
    });
    endpoints.MapGet("/notifications", async context =>
    {
        context.Response.Headers.Add("Content-Type", "application/json");
        await context.Response.WriteAsync(JsonSerializer.Serialize(_databaseService.GetAllNotifications()));
    });
});

app.Run();
