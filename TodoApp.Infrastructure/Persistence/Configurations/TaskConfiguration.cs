using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoApp.Infrastructure.Persistence.Configurations;

internal class TaskConfiguration : IEntityTypeConfiguration<Domain.Entities.Task>
{
	public void Configure(EntityTypeBuilder<Domain.Entities.Task> builder)
	{
		builder
			.ToTable("Tasks");

		builder
			.Property(x => x.Title)
			.HasMaxLength(100)
			.IsRequired();

		builder
			.Property(x => x.Description)
			.HasMaxLength(500);
	}
}