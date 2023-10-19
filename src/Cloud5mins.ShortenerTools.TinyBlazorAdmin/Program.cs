using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;
using Cloud5mins.ShortenerTools.TinyBlazorAdmin;
using AzureStaticWebApps.Blazor.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var baseAddress = builder.HostEnvironment.BaseAddress;

// Get function key from environment variables
var functionKey = Environment.GetEnvironmentVariable("AZURE_FUNCTION_KEY");

builder.Services.AddScoped(sp =>
{
	var httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
	if (!string.IsNullOrEmpty(functionKey))
	{
		httpClient.DefaultRequestHeaders.Add("x-functions-key", functionKey);
	}
	else
	{
		// Log a message if function key is empty or not set
		Console.WriteLine("WARNING: Azure Function key is empty or not set.");
	}
	return httpClient;
});

builder.Services.AddStaticWebAppsAuthentication();


// builder.Services.AddMsalAuthentication(options =>
// {
//     builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
// });

// regiser fusion blazor service
// Community Licence for your personal use ONLY. Thank you Syncfusion for this generous offer.
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzYyMzI1QDMyMzAyZTMxMmUzMFY0cEZ3MVozdkwvekVhek8xTWdPMkg2NlhvdVFNR1lvZHdhQWJWUlNjZW89"); 
builder.Services.AddSyncfusionBlazor();

await builder.Build().RunAsync();
