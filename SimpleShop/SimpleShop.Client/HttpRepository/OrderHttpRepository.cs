using System.Net.Http.Json;
using SimpleShop.Client.HttpRepository.Interfaces;
using SimpleShop.Shared.Orders.Commands;

namespace SimpleShop.Client.HttpRepository;

public class OrderHttpRepository(HttpClient client) : IOrderHttpRepository
{
	public async Task Add(AddOrderCommand command)
		=> await client.PostAsJsonAsync("orders", command);

	public async Task Confirm(ConfirmOrderCommand command)
		=> await client.PostAsJsonAsync("orders/confirm", command);
}