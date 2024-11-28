using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using TodoApp.Client.HttpRepository.Interfaces;
using TodoApp.Client.Services.Interfaces;
using TodoApp.Shared.Tasks.Dtos;

namespace TodoApp.Client.Pages;

public partial class Tasks
{
	private IList<TaskDto> _tasks;
	private bool _showDeleteDialog;
	private string _deleteDialogBody;
	private int _deleteTaskId;

	[Inject]
	public ITaskHttpRepository TaskHttpRepository { get; set; }

	[Inject]
	public IToastrService ToastrService { get; set; }

	protected override async Task OnInitializedAsync()
		=> await RefreshTasks();

	private async Task RefreshTasks() => _tasks = await TaskHttpRepository.GetAll();// do pokazania jak działa wirtualizacja danych //var tasks = await TaskHttpRepository.GetAll();//_tasks = [];//for (int i = 0; i < 100; i++)//{//	_tasks.AddRange(tasks);//}

	private void DeleteTask(int id, string title)
	{
		_deleteDialogBody = $"Czy na pewno chcesz usunąć zadanie: {title}";
		_showDeleteDialog = true;
		_deleteTaskId = id;
	}

	private async Task DeleteConfirmed(MouseEventArgs e)
	{
		await TaskHttpRepository.Delete(_deleteTaskId);
		await ToastrService.ShowSuccessMessage("Zadanie zostało usunięte.");
		await RefreshTasks();
		_showDeleteDialog = false;
	}

	private void DeleteCanceled(MouseEventArgs e)
		=> _showDeleteDialog = false;
}