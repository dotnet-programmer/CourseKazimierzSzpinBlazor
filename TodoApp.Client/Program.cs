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
builder.Services.AddHttpClient("TodoAppAPI", client =>
{
	client.BaseAddress = new Uri(builder.Configuration["ApiConfiguration:BaseAddress"] + "/api/");
	client.Timeout = TimeSpan.FromMinutes(5);
});
builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("TodoAppAPI"));
// maj¹c gotow¹ konfiguracjê mo¿na wstrzykiwaæ implementacjê HttpClienta bezpoœrednio do komponentu,
// ale najlepsz¹ praktyk¹ jest stworzenie osobnej warstwy, takiego nowego serwisu,
// w którym bêd¹ wszystkie wywo³ania endpointów z WebApi z wykorzystaniem HttpClienta
// zwiêkszy to porz¹dek w kodzie i samo u¿ycie bêdzie prostsze, bêdzie mo¿na operowaæ na interfejsach i serwisy mo¿na u¿ywaæ w dowolnych komponentach
// tutaj znajduje siê w folderze HttpRepository

// wstrzykiwane w komponentach, gdzie bêd¹ wywo³ywane endpointy WebApi
builder.Services.AddScoped<ITaskHttpRepository, TaskHttpRepository>();

builder.Services.AddScoped<IToastrService, ToastrService>();

await builder.Build().RunAsync();