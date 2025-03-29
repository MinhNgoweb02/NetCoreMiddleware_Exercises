using Elasticsearch.Net;
using Nest;

var builder = WebApplication.CreateBuilder(args);

// Read from appsettings.json
var elasticUrl = builder.Configuration["Elasticsearch:Url"];
var username = builder.Configuration["Elasticsearch:Username"];
var password = builder.Configuration["Elasticsearch:Password"];
var indexName = builder.Configuration["Elasticsearch:Index"];

// connect to Elasticsearch
var settings = new ConnectionSettings(new Uri(elasticUrl ?? string.Empty))
    .BasicAuthentication(username, password)
    .DefaultIndex("request-logs");  

var client = new ElasticClient(settings);

builder.Services.AddSingleton<IElasticClient>(client);

var app = builder.Build();

app.UseMiddleware<LoggingMiddleware>();

// Check connect Elasticsearch
app.MapGet("/elastic-check", async (IElasticClient client) =>
{
    var response = await client.PingAsync();
    return response.IsValid ? "Elasticsearch is connected!" : $"Connection failed: {response.ServerError?.Error?.Reason}";
});


app.MapGet("/", () => 
    "I am Minh Van Ngo\n" +
    "I am a Software Engineer\n" +
    "This is my assignment for today - AspnetCoreMiddleware"
);

app.Run();
