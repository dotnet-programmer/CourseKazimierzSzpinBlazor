using Microsoft.EntityFrameworkCore;

namespace TodoApp.Application.Common.Interfaces;

public interface IApplicationDbContext : IDisposable
{
	DbSet<Domain.Entities.Task> Tasks { get; set; }

	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}