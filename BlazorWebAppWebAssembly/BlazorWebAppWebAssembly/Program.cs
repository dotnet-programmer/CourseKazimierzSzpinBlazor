using BlazorWebAppWebAssembly.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveWebAssemblyRenderMode()
	// INFO - dzi�ki temu wpisowi mo�na tworzy� oddzielne strony wraz z routingiem w aplikacji klienta i b�dzie to dzia�a�o
	// bez tej linni serwer nie widzia�by routingu klienta
	.AddAdditionalAssemblies(typeof(BlazorWebAppWebAssembly.Client._Imports).Assembly);

app.Run();
