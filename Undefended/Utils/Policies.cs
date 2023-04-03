using Microsoft.AspNetCore.Authorization;
using Undefended.Extensions;

namespace Undefended.Utils;

internal static class Policies {
	public const string AdminClaim = "AdminClaim";

	public static void LoggedPolicy(AuthorizationPolicyBuilder policy) {
		policy.RequireAuthenticatedUser();
	}

	public static void AdminPolicy(AuthorizationPolicyBuilder policy) {
		policy.RequireClaim(AdminClaim);
	}

	public static void AddPolicies(AuthorizationOptions options) {
		options
		   .Add(LoggedPolicy)
		   .Add(AdminPolicy);
	}
}
