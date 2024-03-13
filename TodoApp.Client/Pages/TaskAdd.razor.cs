using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TodoApp.Client.HttpRepository.Interfaces;
using TodoApp.Client.Services.Interfaces;
using TodoApp.Shared.Tasks.Commands;

namespace TodoApp.Client.Pages;

public partial class TaskAdd
{
	private bool _isLoading = false;
	private string _imageFullUrl;
	private AddTaskCommand _addTaskCommand = new() { Term = DateTime.Now };

	[Inject]
    public ITaskHttpRepository TaskHttpRepository { get; set; }

	[Inject]
	public NavigationManager NavigationManager { get; set; }

	[Inject]
	public IToastrService ToastrService { get; set; }

	[Inject]
	public IConfiguration Configuration { get; set; }

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

	private async Task LoadFiles(InputFileChangeEventArgs e)
	{
		var selectedFile = e.File;

		if (selectedFile == null)
		{
			return;
		}

		await TaskHttpRepository.UploadImage(selectedFile);

		_imageFullUrl = $"{Configuration["ApiConfiguration:BaseAddress"]}/content/files/{selectedFile.Name}";

		_addTaskCommand.ImageUrl = _imageFullUrl;
	}
}