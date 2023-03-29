using Defended.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Defended.Utils;

internal static class Policies {
	public static void LoggedPolicy(AuthorizationPolicyBuilder policy) {
		policy.RequireAuthenticatedUser();
	}

	public static void AdminPolicy(AuthorizationPolicyBuilder policy) {
		policy.RequireClaim("AdminClaim");
	}

	public static void AddPolicies(AuthorizationOptions options) {
		options
		   .Add(LoggedPolicy)
		   .Add(AdminPolicy);
	}
}
