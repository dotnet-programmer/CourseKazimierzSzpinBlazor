using Microsoft.AspNetCore.Components;
using TodoApp.Client.HttpRepository.Interfaces;
using TodoApp.Client.Services.Interfaces;
using TodoApp.Shared.Tasks.Commands;

namespace TodoApp.Client.Pages;

public partial class TaskEdit
{
	private bool _isLoading = false;
	private EditTaskCommand _editTaskCommand = new() { Term = DateTime.Now };

	[Parameter]
	public int Id { get; set; }

	[Inject]
	public ITaskHttpRepository TaskHttpRepository { get; set; }

	[Inject]
	public NavigationManager NavigationManager { get; set; }

	[Inject]
	public IToastrService ToastrService { get; set; }

	protected override async Task OnInitializedAsync()
	{
		_editTaskCommand = await TaskHttpRepository.GetEdit(Id);
	}

	private async Task Save()
	{
		try
		{
			_isLoading = true;
			await TaskHttpRepository.Edit(_editTaskCommand);
			NavigationManager.NavigateTo("/");
			await ToastrService.ShowSuccessMessage("Zadanie zostało zaktualizowane.");
		}
		finally
		{
			_isLoading = false;
		}
	}
}