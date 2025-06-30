using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using SimpleShop.Client.Services;
using Toolbelt.Blazor;

namespace SimpleShop.Client.HttpInterceptor;

// logika Interceptora - nuget: Toolbelt.Blazor.HttpClientInterceptor
// Interceptor będzie przechwytywał odpowiedzi z WebApi i będzie je obsługiwał w zależności od odpowiedzi
// wywołując request do WebAPI dobrze jest mieć możliwość wywołania odpowiedniego zachowania na podstawie odpowiedzi jaką otrzymamy z API
// np. jeśli zostanie zwrócony Bad Request to wyświetli się informacja użytkownikowi w toastr,
// albo w przypadku błędu autoryzacji przenieść użyszkodnika do ekranu logowania
public class HttpInterceptorService(
	HttpClientInterceptor interceptor,
	NavigationManager navigationManager,
	RefreshTokenService refreshTokenService)
{
	private readonly HttpClientInterceptor _interceptor = interceptor;
	private readonly NavigationManager _navigationManager = navigationManager;
	private readonly RefreshTokenService _refreshTokenService = refreshTokenService;

	public void RegisterBeforeSendEvent()
	=> _interceptor.BeforeSendAsync += InterceptBeforeSendAsync;

	// udostępnienie metody HandleResponse, żeby można było zarejestrować zdarzenie AfterSendAsync
	// w komponentach gdzie zostanie wywołana ta metoda, to logika z HandleResponse zostanie wykonana po każdym requeście
	public void RegisterAfterSendEvent()
		=> _interceptor.AfterSendAsync += HandleResponse;

	// odpięcie zdarzenia na dispose
	public void DisposeEvent()
	{
		_interceptor.BeforeSendAsync -= InterceptBeforeSendAsync;
		_interceptor.AfterSendAsync -= HandleResponse;
	}

	// zdarzenie wykonywane przed requestem, czyli przed wysłaniem żądania do WebApi, żeby zaktualizować tokena jeśli będzie taka potrzeba
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
		// weryfikacja odpowiedzi, jeśli jest nullem to przekierowanie to strony (widoku) błędu
		if (e.Response == null)
		{
			_navigationManager.NavigateTo("/error");
			return;
		}

		// jeśli odpowiedź nie jest pozytywna - request nie zakończył się sukcesem, czyli był jakiś błąd w odpowiedzi
		if (!e.Response.IsSuccessStatusCode)
		{
			string message;

			switch (e.Response.StatusCode)
			{
				case System.Net.HttpStatusCode.NotFound:
					_navigationManager.NavigateTo("/404");
					message = "Nie znaleziono zasobu.";
					break;
				case HttpStatusCode.Unauthorized:
					_navigationManager.NavigateTo("/logowanie");
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