using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Undefended.Services.Interfaces;

namespace Undefended.Pages.Admins.Permissions;

public class DeleteModel : PageModel {
	private readonly IUsersService              _usersService;
	public           ICollection<IdentityUser>? Users           { get; set; }
	public           IdentityUser?              PermissionsUser { get; set; }
	public           ICollection<Claim>?        Claims          { get; set; }

	public DeleteModel(IUsersService usersService) {
		_usersService = usersService;
	}

	public async Task<IActionResult> OnGetAsync(int userId, string type) {
		await _usersService.RemoveClaimFromUserAsync(userId, type);

		return RedirectToPage("./Index", new { userId });
	}
}
