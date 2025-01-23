using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using TodoApp.Client.HttpRepository.Interfaces;
using TodoApp.Shared.Tasks.Commands;
using TodoApp.Shared.Tasks.Dtos;

namespace TodoApp.Client.HttpRepository;

public class TaskHttpRepository(HttpClient httpClient) : ITaskHttpRepository
{
	private readonly HttpClient _httpClient = httpClient;

	// https://localhost:7214 - bazowy adres Api pobrany z konfiguracji
	// /api/ - dodanie tego doklejenia w Program.cs przy konfiguracji serwisów
	// tasks - nazwa endpointa z WebApi który ma zostać wykonany
	// command - parametr który ma zostać przekazany
	// efekt - https://localhost:7214/api/tasks - POST
	public async Task Add(AddTaskCommand command)
		=> await _httpClient.PostAsJsonAsync("tasks", command);

	public async Task Delete(int id)
		=> await _httpClient.DeleteAsync($"tasks/{id}");

	public async Task Edit(EditTaskCommand command)
		=> await _httpClient.PutAsJsonAsync("tasks", command);

	public async Task<IList<TaskDto>> GetAll()
		=> await _httpClient.GetFromJsonAsync<IList<TaskDto>>("tasks");

	public async Task<EditTaskCommand> GetEdit(int id)
		=> await _httpClient.GetFromJsonAsync<EditTaskCommand>($"tasks/edit/{id}");

	public async Task UploadImage(IBrowserFile file)
	{
		MultipartFormDataContent content = new();
		content.Add(new StreamContent(file.OpenReadStream(file.Size)), "image", file.Name);
		await _httpClient.PostAsync("tasks/upload-image", content);
	}
}