using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using TodoApp.Client.HttpRepository.Interfaces;
using TodoApp.Client.Services.Interfaces;
using TodoApp.Shared.Tasks.Dtos;

namespace TodoApp.Client.Pages;

public partial class Tasks
{
	private const string Title = "Moje zadania";

	private IList<TaskDto> _tasks;
	private bool _showDeleteDialog;
	private string _deleteDialogBody;
	private int _deleteTaskId;

	[Inject]
	public ITasksHttpRepository TasksHttpRepository { get; set; }

	[Inject]
	public IToastrService ToastrService { get; set; }

	protected override async Task OnInitializedAsync()
		=> await RefreshTasks();

	private async Task RefreshTasks() 
		=> _tasks = await TasksHttpRepository.GetAllAsync();
	// do pokazania jak działa wirtualizacja danych 
	//{
	//var tasks = await TaskHttpRepository.GetAll();
	//_tasks = [];
	//for (int i = 0; i < 100; i++)
	//{
	//	_tasks.AddRange(tasks);
	//}
	//}

	private void DeleteTask(int id, string title)
	{
		_deleteDialogBody = $"Czy na pewno chcesz usunąć zadanie: {title}";
		_deleteTaskId = id;
		_showDeleteDialog = true;
	}

	private async Task DeleteConfirmed(MouseEventArgs e)
	{
		await TasksHttpRepository.DeleteAsync(_deleteTaskId);
		await RefreshTasks();
		_showDeleteDialog = false;
		await ToastrService.ShowSuccessMessageAsync("Zadanie zostało usunięte.");
	}

	private void DeleteCanceled(MouseEventArgs e)
		=> _showDeleteDialog = false;
}