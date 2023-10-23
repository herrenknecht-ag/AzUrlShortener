using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;
using Cloud5mins.ShortenerTools.TinyBlazorAdmin;
using AzureStaticWebApps.Blazor.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var baseAddress = builder.HostEnvironment.BaseAddress;

var configuration = new ConfigurationBuilder()
	.Build();

var azFunctionKey = builder.Configuration["AzFunctionKey"];

builder.Services.AddScoped(sp =>
{
	var httpClient = new HttpClient { BaseAddress = new Uri("https://bauera.free.beeceptor.com") };
	if (!string.IsNullOrEmpty(azFunctionKey))
	{
		httpClient.DefaultRequestHeaders.Add("x-functions-key", azFunctionKey);
	}
	else
	{
		httpClient.DefaultRequestHeaders.Add("x-functions-key", "emptykey");
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
