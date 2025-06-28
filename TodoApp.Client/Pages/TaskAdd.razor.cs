using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TodoApp.Client.HttpRepository.Interfaces;
using TodoApp.Client.Services.Interfaces;
using TodoApp.Shared.Tasks.Commands;

namespace TodoApp.Client.Pages;

public partial class TaskAdd
{
	private const string Title = "Dodaj nowe zadanie";

	// model dla widoku z projektu Shared
	private readonly AddTaskCommand _addTaskCommand = new() { Term = DateTime.Now };

	// pole potrzebne do zmiany stanu przycisku Zapisz
	private bool _isLoading = false;

	// do wyświetlenia zdjęcia na formularzu
	private string _imageFullUrl;

	[Inject]
	public ITasksHttpRepository TasksHttpRepository { get; set; }

	[Inject]
	public NavigationManager NavigationManager { get; set; }

	[Inject]
	public IToastrService ToastrService { get; set; }

	[Inject]
	public IConfiguration Configuration { get; set; }

	private async Task SaveAsync()
	{
		try
		{
			_isLoading = true;
			await TasksHttpRepository.AddAsync(_addTaskCommand);
			NavigationManager.NavigateTo("/");
			await ToastrService.ShowSuccessMessageAsync("Nowe zadanie zostało dodane.");
		}
		finally
		{
			_isLoading = false;
		}
	}

	private async Task LoadFiles(InputFileChangeEventArgs e)
	{
		IBrowserFile selectedFile = e.File;

		if (selectedFile == null)
		{
			return;
		}

		await TasksHttpRepository.UploadImageAsync(selectedFile);

		// wyświetlenie zdjęcia z serwera innego projektu na formularzu
		_imageFullUrl = $"{Configuration["ApiConfiguration:BaseAddress"]}/content/files/{selectedFile.Name}";

		_addTaskCommand.ImageUrl = _imageFullUrl;
	}
}