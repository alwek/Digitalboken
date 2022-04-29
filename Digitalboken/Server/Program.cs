using Digitalboken.Server.Interfaces;
using Digitalboken.Server.Repositories;
using Digitalboken.Server.Services;
using Elastic.Apm.NetCoreAll;
using Microsoft.Azure.Cosmos;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System.Security.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel(kestrelOptions =>
{
    kestrelOptions.ConfigureHttpsDefaults(httpsOptions =>
    {
        httpsOptions.SslProtocols = SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13;
    });
});

// Configure logging
ConfigureLogging();
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "DigitalbokenRedis";
});
builder.Services.AddHttpClient<IGoogleSearchService, GoogleSearchService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("GoogleSearch:Url"));
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("key", builder.Configuration.GetValue<string>("GoogleSearch:ApiKey"));
    client.DefaultRequestHeaders.Add("cx", builder.Configuration.GetValue<string>("GoogleSearch:SearchEngineId"));
});
builder.Services.AddSingleton<IRedisCacheService, RedisCacheService>();
builder.Services.AddSingleton<IDocumentRepository, DocumentRepository>();
builder.Services.AddSingleton<ISearchRepository, SearchRepository>();
builder.Services.AddSingleton<CosmosClient>(new CosmosClient(builder.Configuration.GetConnectionString("AzureCosmosDb")));

// Use Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsOrigins",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

// Add apm hosts to the container.
builder.Host.UseAllElasticApm();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || 
    app.Environment.IsEnvironment("Local") || 
    app.Environment.IsEnvironment("Docker"))
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseCors("CorsOrigins");

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

void ConfigureLogging()
{
    var environment = Environment.GetEnvironmentVariable("ENVIRONMENT_NAME");
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environment}.json", optional: true)
        .Build();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureElasticSink(configuration))
        .WriteTo.Http(configuration["Logstash:Uri"])
        .ReadFrom.Configuration(configuration)
        .Enrich.WithProperty("Environment", environment)
        .CreateLogger();

    Serilog.Debugging.SelfLog.Enable(Console.Error);
}

ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration) => 
    new(new Uri(configuration["Elasticsearch:Uri"])) 
    {
        ModifyConnectionSettings = x => x.BasicAuthentication("elastic", "changeme"),
        TypeName = null, // <<<<<<---------------------- MÅSTE FINNAS <<<<--------------------
        AutoRegisterTemplate = bool.Parse(configuration["Elasticsearch:AutoRegisterTemplate"]),
        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
        IndexFormat = configuration["Elasticsearch:IndexFormat"],
        TemplateName = configuration["Elasticsearch:TemplateName"],
        MinimumLogEventLevel = Serilog.Events.LogEventLevel.Debug
    };