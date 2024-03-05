using BlazorWebAssembly;
using BlazorWebAssembly.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// sterowanie sekcj¹ Head w index.html dla poszczególnych stron
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// zarejestrowanie serwisu IStudentRepo w DI
builder.Services.AddScoped<IStudentRepo, StudentRepo>();

// zarejestrowanie serwisu IToastrService - biblioteka JS - ToastR dzi³aj¹ca w Blazorze
builder.Services.AddScoped<IToastrService, ToastrService>();

await builder.Build().RunAsync();
