using Microsoft.AspNetCore.Components;
using TodoApp.Client.HttpRepository.Interfaces;
using TodoApp.Client.Services.Interfaces;
using TodoApp.Shared.Tasks.Commands;

namespace TodoApp.Client.Pages;

public partial class TaskAdd
{
	private bool _isLoading = false;
	private AddTaskCommand _addTaskCommand = new() { Term = DateTime.Now };

	[Inject]
    public ITaskHttpRepository TaskHttpRepository { get; set; }

	[Inject]
	public NavigationManager NavigationManager { get; set; }

	[Inject]
	public IToastrService ToastrService { get; set; }

	private async Task Save()
	{
		try
		{
			_isLoading = true;
			await TaskHttpRepository.Add(_addTaskCommand);
			NavigationManager.NavigateTo("/");
			await ToastrService.ShowSuccessMessage("Nowe zadanie zostało dodane.");
		}
		finally
		{
			_isLoading = false;
		}
	}
}