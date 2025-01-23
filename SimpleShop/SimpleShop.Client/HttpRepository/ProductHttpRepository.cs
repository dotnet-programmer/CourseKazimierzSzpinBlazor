using System.Net.Http.Json;
using SimpleShop.Shared.Common.Models;
using SimpleShop.Shared.Products.Dtos;

namespace SimpleShop.Client.HttpRepository.Interfaces;

// HttpClient służy do wywoływania endpointów z WebApi
public class ProductHttpRepository(HttpClient client) : IProductHttpRepository
{
	public async Task<PaginatedList<ProductDto>> GetAll(int pageNumber, string orderInfo, string searchValue)
		=> await client.GetFromJsonAsync<PaginatedList<ProductDto>>($"products?pageNumber={pageNumber}&orderInfo={orderInfo}&searchValue={searchValue}");
}