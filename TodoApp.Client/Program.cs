using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TodoApp.Client;
using TodoApp.Client.HttpRepository;
using TodoApp.Client.HttpRepository.Interfaces;
using TodoApp.Client.Services;
using TodoApp.Client.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// zwyk�a konfiguracja HttpClient jako Scope nie jest najlepsz� praktyk�
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// najlepiej skorzysta� z fabryki, dodaj�c NuGet: Microsoft.Extensions.Http:
// pierwszy parametr to nazwa po kt�rej b�dzie odniesienie w kodzie
// drugi parametr to konfiguracja
builder.Services.AddHttpClient("TodoAppAPI", client =>
{
	client.BaseAddress = new Uri(builder.Configuration["ApiConfiguration:BaseAddress"] + "/api/");
	client.Timeout = TimeSpan.FromMinutes(5);
});
builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("TodoAppAPI"));
// maj�c gotow� konfiguracj� mo�na wstrzykiwa� implementacj� HttpClienta bezpo�rednio do komponentu,
// ale najlepsz� praktyk� jest stworzenie osobnej warstwy, takiego nowego serwisu,
// w kt�rym b�d� wszystkie wywo�ania endpoint�w z WebApi z wykorzystaniem HttpClienta
// zwi�kszy to porz�dek w kodzie i samo u�ycie b�dzie prostsze, b�dzie mo�na operowa� na interfejsach i serwisy mo�na u�ywa� w dowolnych komponentach
// tutaj znajduje si� w folderze HttpRepository

// wstrzykiwane w komponentach, gdzie b�d� wywo�ywane endpointy WebApi
builder.Services.AddScoped<ITaskHttpRepository, TaskHttpRepository>();

builder.Services.AddScoped<IToastrService, ToastrService>();

await builder.Build().RunAsync();