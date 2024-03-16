﻿using System.Net.Http.Json;
using SimpleShop.Client.HttpRepository.Interfaces;
using SimpleShop.Shared.Orders.Commands;

namespace SimpleShop.Client.HttpRepository;

public class OrderHttpRepository : IOrderHttpRepository
{
	private readonly HttpClient _client;

	public OrderHttpRepository(HttpClient client)
		=> _client = client;

	public async Task Add(AddOrderCommand command)
		=> await _client.PostAsJsonAsync("orders", command);

	public async Task Confirm(ConfirmOrderCommand command)
		=> await _client.PostAsJsonAsync("orders/confirm", command);
}