﻿using Microsoft.AspNetCore.Components.Authorization;
using SimpleShop.Client.HttpRepository.Interfaces;

namespace SimpleShop.Client.Services;

public class RefreshTokenService
{
	private readonly AuthenticationStateProvider _authenticationStateProvider;
	private readonly IAuthenticationHttpRepository _authenticationHttpRepository;

	public RefreshTokenService(AuthenticationStateProvider authenticationStateProvider, IAuthenticationHttpRepository authenticationHttpRepository)
	{
		_authenticationStateProvider = authenticationStateProvider;
		_authenticationHttpRepository = authenticationHttpRepository;
	}

	public async Task<string> TryRefreshToken()
	{
		try
		{
			var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
			var user = authState.User;
			var expClaim = user.FindFirst(c => c.Type.Equals("exp")).Value;
			var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(expClaim));
			var diff = expTime - DateTime.UtcNow;

			return diff.TotalMinutes <= 2 
				? await _authenticationHttpRepository.RefreshToken() 
				: string.Empty;
		}
		catch (Exception)
		{
			return string.Empty;
		}
	}
}