using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using XiloWeb.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<XiloWeb.App>("#app");

// HttpClient pointing to the XiloWeb.Api backend
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? builder.HostEnvironment.BaseAddress;

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });
builder.Services.AddSingleton<TranslationService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ContactApiService>();

await builder.Build().RunAsync();
