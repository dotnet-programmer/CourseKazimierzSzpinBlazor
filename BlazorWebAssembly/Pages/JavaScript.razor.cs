using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorWebAssembly.Pages;

public partial class JavaScript
{
	[Inject]
	public IJSRuntime JSRuntime { get; set; }

	// wywołanie funkcji JS bez zwracanych danych
	private async Task DisplayAlert()
	{
		// pierwszy parametr to nazwa funkcji JS która ma zostać wywołana, a kolenje to lista parametrów przekazanych do tej metody
		await JSRuntime.InvokeVoidAsync("alert", "Przykładowa wiadomość");
	}

	// wywołanie funkcji JS ze zwracaniem danych
	private bool _dialogResult = false;
	private async Task DisplayConfirmDialog ()
	{
		// TValue oznacza typ wartości zwracanej
		_dialogResult = await JSRuntime.InvokeAsync<bool>("confirm", "Czy na pewno chcesz usunąć rekord?");
	}

	// wywołanie własnej funkcji napisanej w JS
	private async Task DisplayResultJS()
	{
		await JSRuntime.InvokeVoidAsync("addNumberJS", 1, 2);
	}

	// dodanie metody w C# i użycie jej w JavaScript
	// taka metoda musi być publiczna i statyczna
	// musi mieć atrybut [JSInvokable], żeby była dostępna w skryptach JS
	[JSInvokable]
	public static int Add(int number1, int number2)
	{
		return number1 + number2;
	}
	private async Task DisplayResultCSharp()
	{
		await JSRuntime.InvokeVoidAsync("addNumberCSharp", 2, 4);
	}
	[JSInvokable]
	public static string GetCurrentDate()
	{
		return DateTime.Now.ToString("d");
	}
	private string _currentDate = string.Empty;
	private async Task DisplayDateCSharp()
	{
		_currentDate = await JSRuntime.InvokeAsync<string>("GetCurrentDateCSharp");
	}

}