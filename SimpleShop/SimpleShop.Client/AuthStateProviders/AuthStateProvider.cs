using System.Net.Http.Headers;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SimpleShop.Client.Models;

namespace SimpleShop.Client.AuthStateProviders;

// klasa z definicją jak ma działać uwierzytelnianie w aplikacji
public class AuthStateProvider : AuthenticationStateProvider
{
	private readonly HttpClient _httpClient;
	private readonly ILocalStorageService _localStorage;
	private readonly AuthenticationState _anonymous;

	public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
	{
		_httpClient = httpClient;
		_localStorage = localStorage;
		_anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
	}

	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		////sztuczny użytkownik do testów
		//var anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
		////return await Task.FromResult(anonymous);
		//var claims = new List<Claim>
		//{
		//	new(ClaimTypes.Name, "k.szpin@wp.pl"),
		//	new(ClaimTypes.Role, "Administrator")
		//};
		//var auth = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "test")));
		//// zwrócenie informacji o tym czy użytkownik zalgowany czy nie
		//return await Task.FromResult(auth);

		var token = await _localStorage.GetItemAsync<string>("authToken");

		if (string.IsNullOrWhiteSpace(token))
		{
			return _anonymous;
		}

		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
		return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
	}

	public void NotifyUserAuthentication(string token)
	{
		ClaimsPrincipal authenticatedUser = new(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));
		var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
		NotifyAuthenticationStateChanged(authState);
	}

	public void NotifyUserLogout()
	{
		var authState = Task.FromResult(_anonymous);
		NotifyAuthenticationStateChanged(authState);
	}
}