using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Undefended.Data.Models;
using Undefended.Extensions;
using Undefended.Services.Interfaces;

namespace Undefended.Pages.Admins.Permissions;

public class IndexModel : PageModel {
	private readonly IUsersService _usersService;

	public ICollection<User>? Users           { get; set; }
	public User?              PermissionsUser { get; set; }

	public IndexModel(IUsersService usersService) {
		_usersService = usersService;
	}

	public async Task<IActionResult> OnGetAsync(int? id) {
		try {
			await Request.RetrieveUserAsync(_usersService);
		} catch (Exception ex) {
			Response.Redirect(Url.RouteUrl("Error", new { message = ex.Message }) ?? "/Error");
			return RedirectToPage("/Error", new { message = ex.Message });
		}

		if (id is null) {
			Users = await _usersService.GetAllUsersAsync();
		} else {
			PermissionsUser = await _usersService.GetUserAsync(id.Value);
		}

		return Page();
	}
}
