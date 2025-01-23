using System.Net.Http.Json;
using SimpleShop.Client.HttpRepository.Interfaces;
using SimpleShop.Shared.Payments.Commands;

namespace SimpleShop.Client.HttpRepository;

public class PaymentHttpRepository(HttpClient client) : IPaymentHttpRepository
{
	public async Task<string> Add(AddPaymentCommand command)
	{
		var response = await client.PostAsJsonAsync("payments", command);
		return await response.Content.ReadAsStringAsync();
	}
}