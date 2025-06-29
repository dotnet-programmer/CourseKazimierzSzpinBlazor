using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SimpleShop.Client;
using SimpleShop.Client.HttpRepository.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// pobranie adresu uri obecnego projektu �eby przekaza� go do konfiguracji HttpClient
var uri = new Uri(builder.Configuration["ApiConfiguration:BaseAddress"] + "api/");

// je�eli jest w��czony prerendering, to ta konfiguracja nie zadzia�a,
// trzeba doda� ja jeszcze raz w 2 projekcie (tym g��wnym, SimpleShop (SSR))
// �eby u�atwi� to zadanie, mo�na zrobi� ca�� konfuguracj� w 1 metodzie rozszerzaj�cej IServiceCollection
// a p�niej w obu projektach doda� wywo�anie tylko tej 1 metody
//builder.Services.AddScoped<IProductHttpRepository, ProductHttpRepository>();
builder.Services.AddClient(uri);

await builder.Build().RunAsync();