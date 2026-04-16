using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PayrollEngine.Web.UI;
using System.Net.Http;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Türkçe kültür ayarı - Sayıları Türkçe formatla göster (978.785,00)
var culture = new CultureInfo("tr-TR");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5032") });

// Logging ekle
builder.Logging.SetMinimumLevel(LogLevel.Debug);

var host = builder.Build();

// Global exception handler
var logger = host.Services.GetRequiredService<ILogger<Program>>();
AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
{
    logger.LogError(error.ExceptionObject as Exception, "Unhandled exception occurred");
};

await host.RunAsync();
