using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Common.Interfaces;
using TodoApp.Shared.Tasks.Dtos;
using TodoApp.Shared.Tasks.Queries;

namespace TodoApp.Application.Tasks.Queries;

public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, IEnumerable<TaskDto>>
{
	private readonly IApplicationDbContext _context;

	public GetTasksQueryHandler(IApplicationDbContext context)
		=> _context = context;

	public async Task<IEnumerable<TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
		=> await _context
			.Tasks
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
			.ToListAsync();
}