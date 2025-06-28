using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using TodoApp.Client.HttpRepository.Interfaces;
using TodoApp.Shared.Tasks.Commands;
using TodoApp.Shared.Tasks.Dtos;

namespace TodoApp.Client.HttpRepository;

public class TasksHttpRepository(HttpClient httpClient) : ITasksHttpRepository
{
	// https://localhost:7214 - bazowy adres Api pobrany z konfiguracji
	// /api/ - dodanie tego doklejenia w Program.cs przy konfiguracji serwisów
	// tasks - nazwa endpointa z WebApi który ma zostać wykonany
	// command - parametr który ma zostać przekazany
	// efekt - https://localhost:7214/api/tasks - POST

	private const string _controller = "tasks";

	public async Task AddAsync(AddTaskCommand command)
		=> await httpClient.PostAsJsonAsync(_controller, command);

	public async Task DeleteAsync(int id)
		=> await httpClient.DeleteAsync($"{_controller}/{id}");

	public async Task EditAsync(EditTaskCommand command)
		=> await httpClient.PutAsJsonAsync(_controller, command);

	public async Task<IList<TaskDto>> GetAllAsync()
		=> await httpClient.GetFromJsonAsync<IList<TaskDto>>(_controller);

	public async Task<EditTaskCommand> GetEditAsync(int id)
		=> await httpClient.GetFromJsonAsync<EditTaskCommand>($"{_controller}/edit/{id}");

	public async Task UploadImageAsync(IBrowserFile file)
	{
		MultipartFormDataContent content = new();
		content.Add(new StreamContent(file.OpenReadStream(file.Size)), "image", file.Name);
		await httpClient.PostAsync($"{_controller}/upload-image", content);
	}
}