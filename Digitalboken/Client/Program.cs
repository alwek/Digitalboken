using Digitalboken.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

string serverHost = string.IsNullOrEmpty(builder.Configuration["SERVER_HOST"]) ?
    builder.HostEnvironment.BaseAddress :
    builder.Configuration["SERVER_HOST"];

builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(serverHost ?? string.Empty) });

await builder.Build().RunAsync();
