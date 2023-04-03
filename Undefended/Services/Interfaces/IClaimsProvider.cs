using System.Security.Claims;

namespace Undefended.Services.Interfaces;

public interface IClaimsProvider {
	public ClaimsPrincipal? ClaimsPrincipal { get; }
}
