using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SimpleShop.Client;
using SimpleShop.Client.HttpRepository.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// pobranie adresu uri obecnego projektu ¿eby przekazaæ go do konfiguracji HttpClient
var uri = new Uri(builder.Configuration["ApiConfiguration:BaseAddress"] + "api/");

// je¿eli jest w³¹czony prerendering, to ta konfiguracja nie zadzia³a,
// trzeba dodaæ ja jeszcze raz w 2 projekcie (tym g³ównym, SimpleShop (SSR))
// ¿eby u³atwiæ to zadanie, mo¿na zrobiæ ca³¹ konfuguracjê w 1 metodzie rozszerzaj¹cej IServiceCollection
// a póŸniej w obu projektach dodaæ wywo³anie tylko tej 1 metody
//builder.Services.AddScoped<IProductHttpRepository, ProductHttpRepository>();
builder.Services.AddClient(uri);

await builder.Build().RunAsync();