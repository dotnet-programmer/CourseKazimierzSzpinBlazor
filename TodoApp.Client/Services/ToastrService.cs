using Microsoft.JSInterop;
using TodoApp.Client.Services.Interfaces;

namespace TodoApp.Client.Services;

public class ToastrService(IJSRuntime jsRuntime) : IToastrService
{
	private readonly IJSRuntime _jsRuntime = jsRuntime;

	public async Task ShowInfoMessage(string message)
		=> await _jsRuntime.InvokeVoidAsync("toastrFunctions.showToastrInfo", message);

	public async Task ShowSuccessMessage(string message)
		=> await _jsRuntime.InvokeVoidAsync("toastrFunctions.showToastrSuccess", message);

	public async Task ShowErrorMessage(string message)
		=> await _jsRuntime.InvokeVoidAsync("toastrFunctions.showToastrError", message);
}