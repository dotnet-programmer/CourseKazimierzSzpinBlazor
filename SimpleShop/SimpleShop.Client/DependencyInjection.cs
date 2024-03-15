using SimpleShop.Client.HttpRepository.Interfaces;

namespace SimpleShop.Client;

public static class DependencyInjection
{
	public static IServiceCollection AddClient(this IServiceCollection services)
	{
		services.AddScoped<IProductHttpRepository, ProductHttpRepository>();

		return services;
	}
}