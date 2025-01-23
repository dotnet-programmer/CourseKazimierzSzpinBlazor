using MediatR;
using TodoApp.Application.Common.Interfaces;
using TodoApp.Shared.Tasks.Commands;

namespace TodoApp.Application.Tasks.Commands;

public class AddTaskCommandHandler(IApplicationDbContext context) : IRequestHandler<AddTaskCommand>
{
	public async Task Handle(AddTaskCommand request, CancellationToken cancellationToken)
	{
		var task = new Domain.Entities.Task
		{
			Description = request.Description,
			Title = request.Title,
			IsExecuted = request.IsExecuted,
			Term = request.Term
		};

		context.Tasks.Add(task);
		await context.SaveChangesAsync(cancellationToken);
	}
}