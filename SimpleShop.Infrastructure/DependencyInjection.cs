using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleShop.Application.Common.Interfaces;
using SimpleShop.Domain.Entities;
using SimpleShop.Infrastructure.Persistence;
using SimpleShop.Infrastructure.Services;

namespace SimpleShop.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

		var connectionString = configuration.GetConnectionString("DefaultConnection");

		services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(connectionString).EnableSensitiveDataLogging());

		services.AddIdentity<ApplicationUser, IdentityRole>(options =>
		{
			options.Lockout.AllowedForNewUsers = true;

			// ustawienie na jak długo ma zostać zablokowane konto
			options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

			// ilość prób nieudanego logowania przed zablokowaniem
			options.Lockout.MaxFailedAccessAttempts = 3;

			// wymagane potwierdzenie konta
			options.SignIn.RequireConfirmedAccount = true;

			// wymagania dotyczące hasła
			options.Password = new PasswordOptions
			{
				RequiredLength = 8,
				RequireDigit = true,
				RequireLowercase = true,
				RequireUppercase = true,
				RequireNonAlphanumeric = true,
			};
		})
		.AddEntityFrameworkStores<ApplicationDbContext>()
		.AddDefaultTokenProviders();

		services.AddScoped<IPaymentService, PaymentService>();

		services.AddScoped<IDateTimeService, DateTimeService>();

		services.AddScoped<IAuthenticationService, AuthenticationService>();

		services.AddScoped<IEmail, Email>();

		return services;
	}
}