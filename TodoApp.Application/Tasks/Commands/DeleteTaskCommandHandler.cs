using MediatR;
using TodoApp.Application.Common.Interfaces;
using TodoApp.Shared.Tasks.Commands;

namespace TodoApp.Application.Tasks.Commands;

public class DeleteTaskCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteTaskCommand>
{
	public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
	{
		context.Tasks.Remove(new Domain.Entities.Task { Id = request.Id });
		await context.SaveChangesAsync(cancellationToken);
	}
}