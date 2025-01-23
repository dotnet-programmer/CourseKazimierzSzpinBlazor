using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Common.Interfaces;
using TodoApp.Shared.Tasks.Commands;
using TodoApp.Shared.Tasks.Queries;

namespace TodoApp.Application.Tasks.Queries;

public class GetEditTaskQueryHandler(IApplicationDbContext context) : IRequestHandler<GetEditTaskQuery, EditTaskCommand>
{
	public async Task<EditTaskCommand> Handle(GetEditTaskQuery request, CancellationToken cancellationToken)
	{
		var task = await context.Tasks
			.AsNoTracking()
			.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

		if (task == null)
		{
			return null;
		}

		return new EditTaskCommand
		{
			Id = request.Id,
			Description = task.Description,
			IsExecuted = task.IsExecuted,
			Term = task.Term,
			Title = task.Title
		};
	}
}