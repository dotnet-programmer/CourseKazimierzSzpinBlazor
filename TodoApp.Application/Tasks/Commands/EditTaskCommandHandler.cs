using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Common.Interfaces;
using TodoApp.Shared.Tasks.Commands;

namespace TodoApp.Application.Tasks.Commands;

public class EditTaskCommandHandler(IApplicationDbContext context) : IRequestHandler<EditTaskCommand>
{
	public async Task Handle(EditTaskCommand request, CancellationToken cancellationToken)
	{
		var task = await context.Tasks
			.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

		if (task == null)
		{
			throw new Exception("notfound");
		}

		task.IsExecuted = request.IsExecuted;
		task.Description = request.Description;
		task.Title = request.Title;
		task.Term = request.Term;

		await context.SaveChangesAsync(cancellationToken);
	}
}