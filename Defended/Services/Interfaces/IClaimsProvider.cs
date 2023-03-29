using System.Security.Claims;

namespace Defended.Services.Interfaces;

public interface IClaimsProvider {
	public ClaimsPrincipal? ClaimsPrincipal { get; }
}
