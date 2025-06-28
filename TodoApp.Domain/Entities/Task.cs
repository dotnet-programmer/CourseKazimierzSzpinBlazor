namespace TodoApp.Domain.Entities;

public class Task
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public bool IsExecuted { get; set; }
	public DateTime? Term { get; set; }
}