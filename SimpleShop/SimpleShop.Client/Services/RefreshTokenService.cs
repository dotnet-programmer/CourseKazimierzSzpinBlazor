using Microsoft.AspNetCore.Components.Authorization;
using SimpleShop.Client.HttpRepository.Interfaces;

namespace SimpleShop.Client.Services;

public class RefreshTokenService(AuthenticationStateProvider authenticationStateProvider, IAuthenticationHttpRepository authenticationHttpRepository)
{
	public async Task<string> TryRefreshToken()
	{
		try
		{
			var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
			var user = authState.User;
			var expClaim = user.FindFirst(c => c.Type.Equals("exp")).Value;
			var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(expClaim));
			var diff = expTime - DateTime.UtcNow;

			return diff.TotalMinutes <= 2
				? await authenticationHttpRepository.RefreshToken()
				: string.Empty;
		}
		catch (Exception)
		{
			return string.Empty;
		}
	}
}