using Microsoft.AspNetCore.Components;
using TodoApp.Client.HttpRepository.Interfaces;
using TodoApp.Shared.Tasks.Dtos;

namespace TodoApp.Client.Pages;

public partial class Tasks
{
	private IList<TaskDto> _tasks;

	[Inject]
	public ITaskHttpRepository TaskHttpRepository { get; set; }

	protected override async Task OnInitializedAsync()
	{
		_tasks = await TaskHttpRepository.GetAll();
	}
}