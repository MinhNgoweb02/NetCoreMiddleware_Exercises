var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (!Directory.Exists("Logs"))
{
    Directory.CreateDirectory("Logs");
}

app.UseMiddleware<LoggingMiddleware>();

app.MapGet("/", () => 
    "I am Minh Van Ngo\n" +
    "I am a Software Engineer\n" +
    "This is my assignment for today - AspnetCoreMiddleware"
);

app.Run();
