using Microsoft.AspNetCore.Components.Web;

namespace BlazorWebAssembly.Layout;

public partial class MainLayout
{
	// obiekt dla sekcji z Id
	public static object TopSection = new();

	// poza wyświetleniem strony z błędem, trzeba stłumić ten wyjątek i przywrócić normalny tryb działania aplikacji
	private ErrorBoundary _errorBoundary;

	// wygaszenie wyjątku
	protected override void OnParametersSet() 
		=> _errorBoundary?.Recover();
}