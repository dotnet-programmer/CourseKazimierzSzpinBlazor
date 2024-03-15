using Microsoft.AspNetCore.Components;
using Toolbelt.Blazor;

namespace SimpleShop.Client.HttpInterceptor;

// INFO - logika Interceptora
public class HttpInterceptorService
{
	private readonly HttpClientInterceptor _interceptor;
	private readonly NavigationManager _navigationManager;

	public HttpInterceptorService(HttpClientInterceptor interceptor, NavigationManager navigationManager)
	{
		_interceptor = interceptor;
		_navigationManager = navigationManager;
	}

	// w komponentach gdzie zostanie wywołana ta metoda, to logika z HandleResponse zostanie wykonana po każdym requeście
	public void RegisterEvent() 
		=> _interceptor.AfterSendAsync += HandleResponse;

	// odpięcie zdarzenia na dispose
	public void DisposeEvent() 
		=> _interceptor.AfterSendAsync -= HandleResponse;

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
				default:
					_navigationManager.NavigateTo("/error");
					message = "Coś poszło nie tak, proszę skontaktuj się z administratorem.";
					break;
			}

			throw new HttpResponseException(message);
		}
	}
}