namespace TodoApp.Shared.Tasks.Dtos;

public class TaskDto
{
	public int Id { get; set; }
	public string Title { get; set; }
	public bool IsExecuted { get; set; }
	public DateTime? Term { get; set; }
}