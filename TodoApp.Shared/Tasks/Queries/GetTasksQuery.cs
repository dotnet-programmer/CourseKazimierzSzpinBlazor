using MediatR;
using TodoApp.Shared.Tasks.Dtos;

namespace TodoApp.Shared.Tasks.Queries;

public class GetTasksQuery : IRequest<IEnumerable<TaskDto>>
{
}