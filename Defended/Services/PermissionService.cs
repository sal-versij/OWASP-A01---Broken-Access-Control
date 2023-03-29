using Defended.Attributes;
using Defended.Data;
using Defended.Exceptions;
using Defended.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Defended.Services;

[Injectable<IPermissionService>]
public class PermissionService : IPermissionService {
	private readonly IClaimsProvider           _claimsProvider;
	private readonly UserManager<IdentityUser> _userManager;
	private readonly ApplicationDbContext      _dbContext;

	public PermissionService(IClaimsProvider claimsProvider, UserManager<IdentityUser> userManager, ApplicationDbContext dbContext) {
		_claimsProvider = claimsProvider;
		_userManager    = userManager;
		_dbContext      = dbContext;
	}

	public string? GetUserId() {
		if (_claimsProvider.ClaimsPrincipal is null) {
			throw new("ClaimsPrincipal is null");
		}

		return _userManager.GetUserId(_claimsProvider.ClaimsPrincipal);
	}

	public async Task<IdentityUser?> GetUser() {
		var userId = GetUserId();
		if (userId is null)
			return null;

		return await _userManager.FindByIdAsync(userId);
	}

	public async Task AssertUserCanAccessResourceAsync(string id) {
		var user = await GetUser();
		if (user is null)
			throw new UserNotLoggedInException("User not found");

		var resource = await _dbContext.Resources.FindAsync(id);
		if (resource is null)
			throw new NotFoundException("Resource not found");

		if (resource.UserId != user.Id)
			throw new ForbiddenException("User doesn't have access to this resource");
	}
}
