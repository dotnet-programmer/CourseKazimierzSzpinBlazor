using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Common.Interfaces;
using TodoApp.Shared.Tasks.Commands;
using TodoApp.Shared.Tasks.Queries;

namespace TodoApp.Application.Tasks.Queries;

public class GetEditTaskQueryHandler : IRequestHandler<GetEditTaskQuery, EditTaskCommand>
{
	private readonly IApplicationDbContext _context;

	public GetEditTaskQueryHandler(IApplicationDbContext context) 
		=> _context = context;

	public async Task<EditTaskCommand> Handle(GetEditTaskQuery request, CancellationToken cancellationToken)
	{
		var task = await _context
			.Tasks
			.AsNoTracking()
			.FirstOrDefaultAsync(x => x.Id == request.Id);

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