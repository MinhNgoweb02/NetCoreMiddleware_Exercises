using System.Text;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var request = context.Request;
        var logData = new StringBuilder();

        logData.AppendLine("---HTTP Request Logging Middleware Information---");
        logData.AppendLine($"Schema: {request.Scheme}");
        logData.AppendLine($"Host: {request.Host}");
        logData.AppendLine($"Path: {request.Path}");
        logData.AppendLine($"QueryString: {request.QueryString}");

        request.EnableBuffering();
        using (var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true))
        {
            var body = await reader.ReadToEndAsync();
            logData.AppendLine($"Request Body: {body}");
            request.Body.Position = 0;  
        }
        logData.AppendLine("--------------------\n");

        await File.AppendAllTextAsync("Logs/requests.log", logData.ToString());

        await _next(context);
    }
}
