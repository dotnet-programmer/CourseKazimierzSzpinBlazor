using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Infrastructure.Configurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder
			.ToTable("Products");

		builder
			.Property(x => x.Name)
			.HasMaxLength(100);

		builder
			.Property(x => x.ImageUrl)
			.HasMaxLength(100);
	}
}