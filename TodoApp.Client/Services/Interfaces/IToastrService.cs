namespace TodoApp.Client.Services.Interfaces;

public interface IToastrService
{
	Task ShowInfoMessage(string message);

	Task ShowSuccessMessage(string message);

	Task ShowErrorMessage(string message);
}