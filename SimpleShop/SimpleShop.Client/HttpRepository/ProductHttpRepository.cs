using System.Net.Http.Json;
using SimpleShop.Shared.Common.Models;
using SimpleShop.Shared.Products.Dtos;

namespace SimpleShop.Client.HttpRepository.Interfaces;

public class ProductHttpRepository : IProductHttpRepository
{
	private readonly HttpClient _client;

	// INFO - HttpClient służy do wywoływania endpointów z WebApi
	public ProductHttpRepository(HttpClient client) 
		=> _client = client;

	public async Task<PaginatedList<ProductDto>> GetAll(int pageNumber, string orderInfo, string searchValue) 
		=> await _client.GetFromJsonAsync<PaginatedList<ProductDto>>($"products?pageNumber={pageNumber}&orderInfo={orderInfo}&searchValue={searchValue}");
}