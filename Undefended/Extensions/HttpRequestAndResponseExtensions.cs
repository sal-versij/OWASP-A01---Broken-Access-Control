using System.Text.Json;
using Undefended.Data.Models;
using Undefended.Services.Interfaces;

namespace Undefended.Extensions;

public static class HttpRequestAndResponseExtensions {
	public static void LoginUser(this HttpResponse response, User user) {
		if (user.Permissions == null)
			throw new ArgumentNullException(nameof(user.Permissions));

		response.Cookies.Append("id",          user.Id.ToString());
		response.Cookies.Append("permissions", JsonSerializer.Serialize(user.Permissions.Select(x => x.Permission)));
	}

	public static void LogoutUser(this HttpResponse response) {
		response.Cookies.Delete("id");
		response.Cookies.Delete("permissions");
	}

	public static Task<User> RetrieveUserAsync(this HttpRequest response, IUsersService usersService) {
		var id = response.Cookies["id"];
		if (id is null)
			throw new ArgumentNullException(nameof(id));

		return usersService.GetUserAsync(int.Parse(id));
	}

	public static ICollection<string> RetrievePermissions(this HttpRequest request) {
		var permissions = request.Cookies["permissions"];
		if (permissions is null)
			return Array.Empty<string>();

		return JsonSerializer.Deserialize<ICollection<string>>(permissions) ?? Array.Empty<string>();
	}

	public static bool HasPermission(this HttpRequest request, string permission) {
		var permissions = request.RetrievePermissions();
		return permissions.Contains(permission);
	}
}
