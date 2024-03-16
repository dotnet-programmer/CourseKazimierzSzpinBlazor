﻿using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using SimpleShop.Client.Services;
using Toolbelt.Blazor;

namespace SimpleShop.Client.HttpInterceptor;

// INFO - logika Interceptora
public class HttpInterceptorService
{
	private readonly HttpClientInterceptor _interceptor;
	private readonly NavigationManager _navigationManager;
	private readonly RefreshTokenService _refreshTokenService;
	private readonly NavigationManager _navManager;

	public HttpInterceptorService(
		HttpClientInterceptor interceptor,
		NavigationManager navigationManager,
		RefreshTokenService refreshTokenService,
		NavigationManager navManager)
	{
		_interceptor = interceptor;
		_navigationManager = navigationManager;
		_refreshTokenService = refreshTokenService;
		_navManager = navManager;
	}

	// w komponentach gdzie zostanie wywołana ta metoda, to logika z HandleResponse zostanie wykonana po każdym requeście
	public void RegisterEvent()
		=> _interceptor.AfterSendAsync += HandleResponse;

	public void RegisterBeforeSendEvent()
		=> _interceptor.BeforeSendAsync += InterceptBeforeSendAsync;

	// odpięcie zdarzenia na dispose
	public void DisposeEvent()
	{
		_interceptor.AfterSendAsync -= HandleResponse;
		_interceptor.BeforeSendAsync -= InterceptBeforeSendAsync;
	}

	private async Task InterceptBeforeSendAsync(object sender, HttpClientInterceptorEventArgs e)
	{
		// sprawdzenie scieżki, która została wywołana
		var absolutePath = e.Request.RequestUri.AbsolutePath;

		// jeżeli nie jest to ścieżka, która zawiera token lub account, czyli jeżeli to nie są metody z kontrolera TokenController lub AccountController z WebApi
		if (!absolutePath.Contains("token") && !absolutePath.Contains("account"))
		{
			var token = await _refreshTokenService.TryRefreshToken();

			if (!string.IsNullOrEmpty(token))
			{
				e.Request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
			}
		}
	}

	// metoda, która będzie wywoływana po każdym requeście, czyli będzie sprawdzana odpowiedź i odpowiednio obsługiwany w zależności od odpowiedzi
	private async Task HandleResponse(object sender, HttpClientInterceptorEventArgs e)
	{
		if (e.Response == null)
		{
			_navigationManager.NavigateTo("/error");
			return;
		}

		var message = string.Empty;

		// jeśli odpowiedź nie jest pozytywna
		if (!e.Response.IsSuccessStatusCode)
		{
			switch (e.Response.StatusCode)
			{
				case System.Net.HttpStatusCode.NotFound:
					_navigationManager.NavigateTo("/404");
					message = "Nie znaleziono zasobu.";
					break;
				case HttpStatusCode.Unauthorized:
					_navManager.NavigateTo("/logowanie");
					message = "Dostęp zabroniony";
					break;
				default:
					_navigationManager.NavigateTo("/error");
					message = "Coś poszło nie tak, proszę skontaktuj się z administratorem.";
					break;
			}

			throw new HttpResponseException(message);
		}
	}
}