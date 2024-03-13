using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Common.Interfaces;

namespace TodoApp.Infrastructure.Persistence;

internal class ApplicationDbContext : DbContext, IApplicationDbContext
{
	public ApplicationDbContext(DbContextOptions options) : base(options)
	{
	}

	public DbSet<Domain.Entities.Task> Tasks { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		
		base.OnModelCreating(modelBuilder);
	}
}