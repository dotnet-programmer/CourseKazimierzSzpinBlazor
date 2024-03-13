using MediatR;
using TodoApp.Shared.Tasks.Commands;

namespace TodoApp.Shared.Tasks.Queries;

public class GetEditTaskQuery : IRequest<EditTaskCommand>
{
	public int Id { get; set; }
}