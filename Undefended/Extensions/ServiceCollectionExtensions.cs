﻿using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Undefended.Attributes;
using Undefended.Data;
using Undefended.Services.Interfaces;

namespace Undefended.Extensions;

public static class ServiceCollectionExtensions {
	private static readonly Assembly Assembly      = Assembly.GetAssembly(typeof(ServiceCollectionExtensions)) ?? throw new InvalidOperationException();
	private static readonly Type[]   AssemblyTypes = Assembly.GetTypes();

	/// <summary>
	///     Load services from namespace Valeat.Services that have the attribute Injectable
	/// </summary>
	/// <param name="services"></param>
	/// <returns></returns>
	public static IServiceCollection AddInjectablesFromAssembly(this IServiceCollection services) {
		foreach(var type in AssemblyTypes) {
			if (type is not { IsInterface: false, IsAbstract: false, IsGenericType: false })
				continue;

			foreach(var attribute in type.GetCustomAttributes<InjectableAttribute>())
				switch (attribute.Lifetime) {
				case ServiceLifetime.Scoped:
					if (attribute.ImplementationType != null)
						services.AddScoped(attribute.ImplementationType, type);
					else
						services.AddScoped(type);

					break;
				case ServiceLifetime.Singleton:
					if (attribute.ImplementationType != null)
						services.AddSingleton(attribute.ImplementationType, type);
					else
						services.AddSingleton(type);

					break;
				case ServiceLifetime.Transient:
					if (attribute.ImplementationType != null)
						services.AddTransient(attribute.ImplementationType, type);
					else
						services.AddTransient(type);

					break;
				}
		}

		return services;
	}

	public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, string? connectionString) {
		if (services == null)
			throw new ArgumentNullException(nameof(services));

		services.AddDbContextFactory<ApplicationDbContext>(options => options.UseSqlite(connectionString))
				.AddDatabaseDeveloperPageExceptionFilter();

		return services;
	}

	public static IServiceCollection AddEndpointsFromAssembly(this IServiceCollection services) {
		foreach(var type in AssemblyTypes) {
			if (type is not { IsInterface: false, IsAbstract: false, IsGenericType: false })
				continue;

			var interfaces = type.GetInterfaces();
			if (!interfaces.Contains(typeof(IEndpoint)))
				continue;

			services.AddScoped(typeof(IEndpoint), type);
		}

		return services;
	}
}
