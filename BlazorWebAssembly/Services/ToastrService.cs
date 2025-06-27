using Microsoft.JSInterop;

namespace BlazorWebAssembly.Services;

// używanie bibliotek JS w C#
// ta klasa jest po to żeby ułatwić dostę do funkcji Toastr
// dzięki niej wystarczy wywołać metodę np. ShowInfoMessage i nie trzeba pisać całego kodu JS w komponentach Blazora
public class ToastrService(IJSRuntime jSRuntime) : IToastrService
{
	public async Task ShowInfoMessage(string message)
		=> await jSRuntime.InvokeVoidAsync("toastrFunctions.showToastrInfo", message);

	public async Task ShowErrorMessage(string message)
		=> await jSRuntime.InvokeVoidAsync("toastrFunctions.showToastrError", message);
}