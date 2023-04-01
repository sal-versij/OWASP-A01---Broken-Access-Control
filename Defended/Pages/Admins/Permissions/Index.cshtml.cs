using System.Security.Claims;
using Defended.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Defended.Pages.Admins.Permissions;

public class IndexModel : PageModel {
	private readonly IPermissionService _permissionService;

	public ICollection<IdentityUser>? Users           { get; set; }
	public IdentityUser?              PermissionsUser { get; set; }
	public ICollection<Claim>?        Claims          { get; set; }


	public IndexModel(IPermissionService permissionService) {
		_permissionService = permissionService;
	}

	public async Task OnGetAsync(string? id) {
		if (id is null) {
			Users = await _permissionService.GetUsersAsync();
		} else {
			PermissionsUser = await _permissionService.GetUserAsync(id);
			Claims          = await _permissionService.GetUserClaimsAsync(id);
		}
	}
}
