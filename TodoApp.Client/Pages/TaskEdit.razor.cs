using Microsoft.AspNetCore.Components;
using TodoApp.Client.HttpRepository.Interfaces;
using TodoApp.Client.Services.Interfaces;
using TodoApp.Shared.Tasks.Commands;

namespace TodoApp.Client.Pages;

public partial class TaskEdit
{
	private const string Title = "Edytuj zadanie";

	private bool _isLoading = false;
	private EditTaskCommand _editTaskCommand = new();

	[Parameter]
	public int Id { get; set; }

	[Inject]
	public ITasksHttpRepository TasksHttpRepository { get; set; }

	[Inject]
	public NavigationManager NavigationManager { get; set; }

	[Inject]
	public IToastrService ToastrService { get; set; }

	protected override async Task OnInitializedAsync() 
		=> _editTaskCommand = await TasksHttpRepository.GetEditAsync(Id);

	private async Task SaveAsync()
	{
		try
		{
			_isLoading = true;
			await TasksHttpRepository.EditAsync(_editTaskCommand);
			NavigationManager.NavigateTo("/");
			await ToastrService.ShowSuccessMessageAsync("Zadanie zostało zaktualizowane.");
		}
		finally
		{
			_isLoading = false;
		}
	}
}