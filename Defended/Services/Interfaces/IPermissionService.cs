using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Defended.Services.Interfaces;

public interface IPermissionService {
	string?                         GetUserId();
	Task<IdentityUser?>             GetCurrentUserAsync();
	Task<IdentityUser?>             GetUserAsync(string       id);
	Task<ICollection<Claim>>        GetUserClaimsAsync(string id);
	Task<ICollection<IdentityUser>> GetUsersAsync();
	Task                            AssertUserCanAccessResourceAsync(string id);
	Task                            AssertUserIsAdminAsync();
	Task                            AssertUserCanAccessAdminResourceAsync(string id);
}
