namespace BlazorWebAssembly.Services;

// używanie bibliotek JS w C#
public interface IToastrService
{
	Task ShowInfoMessage(string message);
}