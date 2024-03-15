namespace SimpleShop.Client.HttpInterceptor;

// INFO - Interceptor - własna klasa wyjątku, żeby odróżnić sytuacje, gdzie został wywołany wyjątek z Interceptora
[Serializable]
public class HttpResponseException : Exception
{
	public HttpResponseException()
	{
	}

	public HttpResponseException(string message) : base(message)
	{
	}
}