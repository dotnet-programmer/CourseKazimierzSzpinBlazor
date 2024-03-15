using SimpleShop.Shared.Common.Models;
using SimpleShop.Shared.Products.Dtos;
using System.Net.Http.Json;

namespace SimpleShop.Client.HttpRepository.Interfaces;

public class ProductHttpRepository : IProductHttpRepository
{
	private readonly HttpClient _client;

	// INFO - HttpClient służy do wywoływania endpointów z WebApi
	public ProductHttpRepository(HttpClient client)
	{
		_client = client;
	}
}