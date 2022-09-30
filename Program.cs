var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

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
