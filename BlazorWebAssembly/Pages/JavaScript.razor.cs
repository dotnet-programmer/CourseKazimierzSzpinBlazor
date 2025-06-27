using BlazorWebAssembly.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorWebAssembly.Pages;

public partial class JavaScript
{
	// właściwość potrzebna jeżeli będzie wywoływany kod JavaScript w Blazorze
	[Inject]
	public IJSRuntime JSRuntime { get; set; }

	// wywołanie funkcji JS bez zwracanych danych
	// pierwszy parametr to nazwa funkcji JS która ma zostać wywołana, a kolejne to lista parametrów przekazanych do tej metody
	private async Task DisplayAlert()
		=> await JSRuntime.InvokeVoidAsync("alert", "Przykładowa wiadomość");

	// wywołanie funkcji JS ze zwracaniem danych
	private bool _dialogResult = false;
	// wywołanie funkcji JS ze zwracaniem danych
	// typ generyczny TValue oznacza typ wartości zwracanej
	private async Task DisplayConfirmDialog()
		=> _dialogResult = await JSRuntime.InvokeAsync<bool>("confirm", "Czy na pewno chcesz usunąć rekord?");

	// wywołanie własnej funkcji napisanej w JS
	// kod JS znajduje się w osobnym pliku .js w folderze wwwrooot/scripts
	// w pliku index.html trzeba dodać odwołanie do tego pliku .js w sekcji ze sktyptami
	private async Task DisplayResultJS()
		=> await JSRuntime.InvokeVoidAsync("addNumberJS", 1, 2);

	#region dodanie metody w C# i użycie jej w JavaScript
	// taka metoda musi być publiczna i statyczna
	// musi mieć atrybut [JSInvokable], żeby była dostępna w skryptach JS
	[JSInvokable]
	public static int Add(int number1, int number2)
		=> number1 + number2;

	private async Task DisplayResultCSharp()
		=> await JSRuntime.InvokeVoidAsync("addNumberCSharp", 2, 4);

	[JSInvokable]
	public static string GetCurrentDate()
		=> DateTime.Now.ToString("d");

	private string _currentDate = string.Empty;

	private async Task DisplayDateCSharp()
		=> _currentDate = await JSRuntime.InvokeAsync<string>("GetCurrentDateCSharp");
	#endregion

	#region moduł JS
	private IJSObjectReference _jsModule;

	protected override async Task OnInitializedAsync()
		=> _jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./scripts/jsTestModule.js");

	private async Task DisplayResultJsModule()
		=> await _jsModule.InvokeVoidAsync("addNumberJSModule", 5, 5);
	#endregion

	#region używanie bibliotek JS w C#
	[Inject]
	public IToastrService ToastrService { get; set; }
	// w przypadku gdy nie ma interfejsu IToastrService, można wstrzyknąć bezpośrednio klasę ToastrService:
	// [Inject]
	// public ToastrService ToastrService { get; set; }

	private async Task DisplayToastrNotification()
		=> await ToastrService.ShowInfoMessage("Toastr wywołany w Blazor!");
	#endregion

	#region praca domowa 2
	private string _newString = string.Empty;

	// TValue oznacza typ wartości zwracanej
	private async Task DisplayNewString()
		=> _newString = await JSRuntime.InvokeAsync<string>("joinStrings", "Funkcja", "String", "-");

	private async Task ChangeBackgroundColor()
		=> await JSRuntime.InvokeVoidAsync("changeBackgroundColor");
	#endregion
}