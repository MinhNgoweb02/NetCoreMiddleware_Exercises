var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (!Directory.Exists("Logs"))
{
    Directory.CreateDirectory("Logs");
}

app.UseMiddleware<LoggingMiddleware>();

app.MapGet("/", () => "Hello World!");

app.Run();
