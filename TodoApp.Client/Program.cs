using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TodoApp.Client;
using TodoApp.Client.HttpRepository;
using TodoApp.Client.HttpRepository.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// zwyk�a konfiguracja HttpClient jako Scope nie jest najlepsz� praktyk�
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// najlepiej skorzysta� z fabryki, dodaj�c NuGet: Microsoft.Extensions.Http:
builder.Services.AddHttpClient("TodoAppAPI", client =>
{
	client.BaseAddress = new Uri(builder.Configuration["ApiConfiguration:BaseAddress"] + "/api/");
	client.Timeout = TimeSpan.FromMinutes(5);
});
builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("TodoAppAPI"));

// wstrzykiwane w komponentach, gdzie b�d� wywo�ywane endpointy WebApi
builder.Services.AddScoped<ITaskHttpRepository, TaskHttpRepository>();

await builder.Build().RunAsync();