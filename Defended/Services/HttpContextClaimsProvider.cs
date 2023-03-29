using System.Security.Claims;
using Defended.Attributes;
using Defended.Services.Interfaces;

namespace Defended.Services;

[Injectable<IClaimsProvider>]
public class HttpContextClaimsProvider : IClaimsProvider {
	public ClaimsPrincipal? ClaimsPrincipal { get; private set; }

	public HttpContextClaimsProvider(IHttpContextAccessor accessor) {
		ClaimsPrincipal = accessor.HttpContext?.User;
	}
}