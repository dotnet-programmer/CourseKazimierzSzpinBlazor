namespace TodoApp.Client.Services.Interfaces;

public interface IToastrService
{
	Task ShowInfoMessageAsync(string message);
	Task ShowSuccessMessageAsync(string message);
	Task ShowErrorMessageAsync(string message);
}