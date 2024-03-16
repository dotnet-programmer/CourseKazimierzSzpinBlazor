using Microsoft.EntityFrameworkCore;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Application.Common.Interfaces;

public interface IApplicationDbContext : IDisposable
{
    DbSet<ApplicationUser> Users { get; set; }	
    DbSet<Product> Products { get; set; }
	DbSet<Order> Orders { get; set; }

	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}