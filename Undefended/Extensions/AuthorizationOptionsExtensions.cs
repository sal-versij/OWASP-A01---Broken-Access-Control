using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Undefended.Extensions;

public static class AuthorizationOptionsExtensions {
	public static AuthorizationOptions Add(this AuthorizationOptions options, Action<AuthorizationPolicyBuilder> configurePolicy, [CallerArgumentExpression(nameof(configurePolicy))] string? policyName = null) {
		if (policyName is null)
			throw new ArgumentNullException(nameof(policyName));

		options.AddPolicy(policyName, configurePolicy);

		return options;
	}
}
