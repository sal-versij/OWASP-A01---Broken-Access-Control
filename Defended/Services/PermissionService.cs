using System.Security.Claims;
using Defended.Attributes;
using Defended.Data;
using Defended.Exceptions;
using Defended.Services.Interfaces;
using Defended.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

	public async Task<IdentityUser?> GetCurrentUserAsync() {
		var userId = GetUserId();
		if (userId is null)
			return null;

		return await _userManager.FindByIdAsync(userId);
	}

	public async Task<IdentityUser?> GetUserAsync(string id) {
		return await _userManager.FindByIdAsync(id);
	}

	public async Task<ICollection<Claim>> GetUserClaimsAsync(string id) {
		var user = await GetUserAsync(id);
		if (user is null)
			throw new NotFoundException("User not found");

		return await _userManager.GetClaimsAsync(user);
	}

	public async Task<ICollection<IdentityUser>> GetUsersAsync() {
		return await _userManager.Users.ToListAsync();
	}

	public async Task AssertUserCanAccessResourceAsync(string id) {
		var user = await GetCurrentUserAsync();
		if (user is null)
			throw new UserNotLoggedInException("User not found");

		var resource = await _dbContext.Resources.FindAsync(id);
		if (resource is null)
			throw new NotFoundException("Resource not found");

		if (resource.UserId != user.Id)
			throw new ForbiddenException("User doesn't have access to this resource");
	}

	public async Task AssertUserIsAdminAsync() {
		var user = await GetCurrentUserAsync();
		if (user is null)
			throw new NotFoundException("User not found");

		var claims = await _userManager.GetClaimsAsync(user);
		if (!claims.Any(x => x is { Type: Policies.AdminClaim, Value: "1" }))
			throw new ForbiddenException("User is not an admin");
	}

	public async Task AssertUserCanAccessAdminResourceAsync(string id) {
		var user = await GetCurrentUserAsync();
		if (user is null)
			throw new NotFoundException("User not found");

		var claims = await _userManager.GetClaimsAsync(user);
		if (!claims.Any(x => x is { Type: Policies.AdminClaim, Value: "1" }))
			throw new ForbiddenException("User is not an admin");

		var resource = await _dbContext.AdminResources.FindAsync(id);
		if (resource is null)
			throw new NotFoundException("Resource not found");

		if (resource.UserId != user.Id)
			throw new ForbiddenException("User doesn't have access to this resource");
	}

	public async Task AddClaimToUserAsync(string userId, string type, string value) {
		await AssertUserIsAdminAsync();
		var user = await GetUserAsync(userId);
		if (user is null)
			throw new NotFoundException("User not found");

		await _userManager.AddClaimAsync(user, new(type, value));
	}

	public async Task RemoveClaimFromUserAsync(string userId, string claimType) {
		await AssertUserIsAdminAsync();
		var user = await GetUserAsync(userId);
		if (user is null)
			throw new NotFoundException("User not found");

		var claims = await _userManager.GetClaimsAsync(user);
		var claim  = claims.FirstOrDefault(x => x.Type == claimType);
		if (claim is null)
			throw new NotFoundException("Claim not found");

		await _userManager.RemoveClaimAsync(user, claim);
	}
}
