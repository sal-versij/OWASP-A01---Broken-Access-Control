using System.Reflection;
using OwaspA01.Services.Interfaces;

namespace OwaspA01.Extensions;

public static class WebApplicationExtensions {
	private static readonly Assembly Assembly      = Assembly.GetAssembly(typeof(ServiceCollectionExtensions)) ?? throw new InvalidOperationException();
	private static readonly Type[]   AssemblyTypes = Assembly.GetTypes();

	// IEndpoint
	public static void MapEndPoints(this WebApplication app) {
		foreach(var type in AssemblyTypes)
			if (type.GetInterface(nameof(IEndpoint)) is not null) {
				var endpoint = Activator.CreateInstance(type) as IEndpoint;
				endpoint?.MapEndPoints(app);
			}
	}
}
