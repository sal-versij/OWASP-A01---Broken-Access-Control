using Microsoft.AspNetCore.Identity;

namespace Defended.Services.Interfaces;

public interface IPermissionService {
	string?             GetUserId();
	Task<IdentityUser?> GetUser();
	Task                AssertUserCanAccessResourceAsync(string id);
}
