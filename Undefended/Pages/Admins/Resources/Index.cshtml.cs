using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Undefended.Data.Models;
using Undefended.Data.Models.Dtos;
using Undefended.Extensions;
using Undefended.Services.Interfaces;

namespace Undefended.Pages.Admins.Resources;

public class IndexModel : PageModel {
	private readonly IAdminResourcesService _resourcesService;
	private readonly IUsersService          _usersService;

	public ICollection<AdminResourceDto>? Resources { get; set; }

	public IndexModel(IAdminResourcesService resourcesService, IUsersService usersService) {
		_resourcesService = resourcesService;
		_usersService     = usersService;
	}

	public async Task<IActionResult> OnGetAsync() {
		User user;
		try {
			user = await Request.RetrieveUserAsync(_usersService);
		} catch (Exception ex) {
			Response.Redirect(Url.RouteUrl("Error", new { message = ex.Message }) ?? "/Error");
			return RedirectToPage("/Error", new { message = ex.Message });
		}

		Resources = await _resourcesService.GetAllAsync<AdminResourceDto>(user.Id);
		return Page();
	}
}
