using BlazorWebAssembly;
using BlazorWebAssembly.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// sterowanie sekcj� Head w index.html dla poszczeg�lnych stron
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// W Blazorze po stronie klienta nie ma poj�cia zakresu, czyli Scoped, tak jak w aplikacjach serwerowych ASP.NET Core.
// Przez to Scoped i Singleton zachowuj� si� tak samo, a konkretnie jako Singleton.

// zarejestrowanie serwisu IStudentRepo w Dependency Injection
builder.Services.AddScoped<IStudentRepo, StudentRepo>();

// zarejestrowanie serwisu IToastrService - biblioteka JS - ToastR dzi�aj�ca w Blazorze
builder.Services.AddScoped<IToastrService, ToastrService>();

await builder.Build().RunAsync();
