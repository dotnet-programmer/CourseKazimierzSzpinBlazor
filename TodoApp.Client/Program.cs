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

// zwyk³a konfiguracja HttpClient jako Scope nie jest najlepsz¹ praktyk¹
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// najlepiej skorzystaæ z fabryki, dodaj¹c NuGet: Microsoft.Extensions.Http:
// pierwszy parametr to nazwa po której bêdzie odniesienie w kodzie
// drugi parametr to konfiguracja
// dodanie fabryki HttpClient:
builder.Services.AddHttpClient("TodoAppAPI", client =>
{
	// konfiguracja adresu URI pobierana z pliku appsettings.json
	// doklejony dodatkowo "/api/" na koñcu, aby wskazaæ na endpointy WebApi i nie trzeba tego robiæ za ka¿dym razem wywo³uj¹c endpointy
	client.BaseAddress = new Uri(builder.Configuration["ApiConfiguration:BaseAddress"] + "/api/");
	client.Timeout = TimeSpan.FromMinutes(5);
});
// utworzenie klienta HttpClienta z tej fabryki
builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("TodoAppAPI"));
// maj¹c gotow¹ konfiguracjê mo¿na wstrzykiwaæ implementacjê HttpClienta bezpoœrednio do komponentu,
// ale najlepsz¹ praktyk¹ jest stworzenie osobnej warstwy, takiego nowego serwisu,
// w którym bêd¹ wszystkie wywo³ania endpointów z WebApi z wykorzystaniem HttpClienta
// zwiêkszy to porz¹dek w kodzie i samo u¿ycie bêdzie prostsze, bêdzie mo¿na operowaæ na interfejsach i serwisy mo¿na u¿ywaæ w dowolnych komponentach
// tutaj znajduje siê w folderze HttpRepository

// wstrzykiwane w komponentach, gdzie bêd¹ wywo³ywane endpointy WebApi
builder.Services.AddScoped<ITasksHttpRepository, TasksHttpRepository>();

builder.Services.AddScoped<IToastrService, ToastrService>();

await builder.Build().RunAsync();