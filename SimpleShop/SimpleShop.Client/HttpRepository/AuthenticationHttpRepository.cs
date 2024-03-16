using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using SimpleShop.Client.HttpRepository.Interfaces;
using SimpleShop.Shared.Authentication.Commands;
using SimpleShop.Shared.Authentication.Dtos;

namespace SimpleShop.Client.HttpRepository;

public class AuthenticationHttpRepository : IAuthenticationHttpRepository
{
	private readonly HttpClient _client;
	private readonly ILocalStorageService _localStorage;
	private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

	public AuthenticationHttpRepository(HttpClient client, ILocalStorageService localStorage)
	{
		_client = client;
		_localStorage = localStorage;
	}

	public async Task<string> RefreshToken()
	{
		var token = await _localStorage.GetItemAsync<string>("authToken");
		var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");
		var response = await _client.PostAsJsonAsync("token/refresh", new RefreshTokenCommand { Token = token, RefreshToken = refreshToken });
		var content = await response.Content.ReadAsStringAsync();
		var result = JsonSerializer.Deserialize<LoginUserDto>(content, _options);

		if (!result.IsAuthSuccessful)
		{
			return null;
		}

		await _localStorage.SetItemAsync("authToken", result.Token);
		await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);
		_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
		return result.Token;
	}
}