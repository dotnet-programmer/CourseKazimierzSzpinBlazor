using Microsoft.JSInterop;

namespace BlazorWebAssembly.Services;

// używanie bibliotek JS w C#
public class ToastrService : IToastrService
{
	private readonly IJSRuntime _jSRuntime;

	public ToastrService(IJSRuntime jSRuntime) 
		=> _jSRuntime = jSRuntime;

	public async Task ShowInfoMessage(string message) 
		=> await _jSRuntime.InvokeVoidAsync("toastrFunctions.showToastrInfo", message);
}
