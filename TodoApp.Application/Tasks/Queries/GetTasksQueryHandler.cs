using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Common.Interfaces;
using TodoApp.Shared.Tasks.Dtos;
using TodoApp.Shared.Tasks.Queries;

namespace TodoApp.Application.Tasks.Queries;

public class GetTasksQueryHandler(IApplicationDbContext context) : IRequestHandler<GetTasksQuery, IEnumerable<TaskDto>>
{
	public async Task<IEnumerable<TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
		=> await context.Tasks
			.AsNoTracking()
			.OrderByDescending(x => x.Term)
			.ThenBy(x => x.Id)
			.Select(x => new TaskDto
			{
				Id = x.Id,
				Title = x.Title,
				IsExecuted = x.IsExecuted,
				Term = x.Term,
			})
			.ToListAsync(cancellationToken);
}