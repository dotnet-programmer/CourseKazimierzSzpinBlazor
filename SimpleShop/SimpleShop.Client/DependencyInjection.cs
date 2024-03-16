using Toolbelt.Blazor.Extensions.DependencyInjection;
using SimpleShop.Client.HttpRepository.Interfaces;
using SimpleShop.Client.HttpInterceptor;
using Blazored.LocalStorage;
using SimpleShop.Client.HttpRepository;

namespace SimpleShop.Client;

public static class DependencyInjection
{
	public static IServiceCollection AddClient(this IServiceCollection services, Uri uri)
	{
		// wersja bez Interceptora
		//services.AddHttpClient("SimpleShopAPI", client =>
		//{
		//	client.BaseAddress = uri;
		//	client.Timeout = TimeSpan.FromMinutes(5);
		//});
		//services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("SimpleShopAPI"));

		// wersja z interceptorem
		// sp - service provider
		services.AddHttpClient("SimpleShopAPI", (sp, client) =>
		{
			client.BaseAddress = uri;
			client.Timeout = TimeSpan.FromMinutes(5);
			client.EnableIntercept(sp);
		});
		services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("SimpleShopAPI"));
		services.AddHttpClientInterceptor();
		services.AddScoped<HttpInterceptorService>();

		services.AddScoped<IProductHttpRepository, ProductHttpRepository>();

		services.AddScoped<IOrderHttpRepository, OrderHttpRepository>();

		services.AddBlazoredLocalStorage();

		return services;
	}
}