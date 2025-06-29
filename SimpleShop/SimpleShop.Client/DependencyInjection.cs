using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SimpleShop.Client.AuthStateProviders;
using SimpleShop.Client.HttpInterceptor;
using SimpleShop.Client.HttpRepository;
using SimpleShop.Client.HttpRepository.Interfaces;
using SimpleShop.Client.Services;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace SimpleShop.Client;

public static class DependencyInjection
{
	// adres uri będzie pobierany w zależności od aplikacji, która będzie korzystać z tego klienta (wywoływać tą metodę)
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

		// serwisy do wywoływania endpointów z WebApi
		services.AddScoped<IProductHttpRepository, ProductHttpRepository>();
		services.AddScoped<IOrderHttpRepository, OrderHttpRepository>();
		services.AddScoped<IPaymentHttpRepository, PaymentHttpRepository>();

		services.AddScoped<IAuthenticationHttpRepository, AuthenticationHttpRepository>();

		services.AddScoped<RefreshTokenService>();

		// NuGet: Blazored.LocalStorage
		services.AddBlazoredLocalStorage();

		// konfiguracja serwisów dla autoryzacji
		services.AddOptions();
		services.AddAuthorizationCore();
		services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
		services.AddCascadingAuthenticationState();

		return services;
	}
}