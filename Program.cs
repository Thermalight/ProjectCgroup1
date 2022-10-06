using ChengetaWebApp.Api.Database;

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

builder.Services.AddSingleton<MqttService>();

var app = builder.Build();

app.Services.GetRequiredService<MqttService>().Start();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        context.Response.Headers.Add("Content-Type", "text/html");
        await context.Response.WriteAsync(System.IO.File.ReadAllText(@"./wwwroot/index.html"));
    });
});

app.Run();
