using System.Security.Claims;
using Defended.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Defended.Pages.Admins.Permissions;

public class DeleteModel : PageModel {
	private readonly IPermissionService _permissionService;

	public ICollection<IdentityUser>? Users           { get; set; }
	public IdentityUser?              PermissionsUser { get; set; }
	public ICollection<Claim>?        Claims          { get; set; }


	public DeleteModel(IPermissionService permissionService) {
		_permissionService = permissionService;
	}

	public async Task<IActionResult> OnGetAsync(string userId, string type) {
		PermissionsUser = await _permissionService.GetUserAsync(userId);

		if (PermissionsUser == null) {
			return NotFound();
		}

		await _permissionService.RemoveClaimFromUserAsync(PermissionsUser.Id, type);

		return RedirectToPage("./Index", new { userId });
	}
}
