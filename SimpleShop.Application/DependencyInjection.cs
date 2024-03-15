﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleShop.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

		return services;
	}
}