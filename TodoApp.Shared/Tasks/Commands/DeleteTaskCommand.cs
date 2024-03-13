using MediatR;

namespace TodoApp.Shared.Tasks.Commands;

public class DeleteTaskCommand : IRequest
{
	public int Id { get; set; }
}