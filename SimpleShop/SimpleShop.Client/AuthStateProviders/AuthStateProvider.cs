using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace SimpleShop.Client.AuthStateProviders;

// klasa z definicją jak ma działać uwierzytelnianie w aplikacji
public class AuthStateProvider : AuthenticationStateProvider
{
	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		//sztuczny użytkownik do testów
		var anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
		//return await Task.FromResult(anonymous);
		var claims = new List<Claim>
		{
			new(ClaimTypes.Name, "k.szpin@wp.pl"),
			new(ClaimTypes.Role, "Administrator")
		};
		var auth = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "test")));
		// zwrócenie informacji o tym czy użytkownik zalgowany czy nie
		return await Task.FromResult(auth);
	}
}