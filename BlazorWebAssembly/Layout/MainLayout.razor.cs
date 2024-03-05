using Microsoft.AspNetCore.Components.Web;

namespace BlazorWebAssembly.Layout;

public partial class MainLayout
{
	// poza wyświetleniem strony z błędem, trzeba stłumić ten wyjątek i przywrócić normalny tryb działania aplikacji
	private ErrorBoundary _errorBoundary;
	protected override void OnParametersSet()
	{
		// wygaszenie wyjątku
		_errorBoundary?.Recover();
	}
}