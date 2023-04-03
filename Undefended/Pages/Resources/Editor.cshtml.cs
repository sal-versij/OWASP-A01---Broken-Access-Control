﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Undefended.Data.Models;
using Undefended.Data.Models.Dtos;
using Undefended.Extensions;
using Undefended.Services.Interfaces;

namespace Undefended.Pages.Resources;

public class EditorModel : PageModel {
	private readonly IResourcesService _resourcesService;
	private readonly IUsersService     _usersService;

	[BindProperty]
	public ResourceDto Resource { get; set; } = new();

	public EditorModel(IResourcesService resourcesService, IUsersService usersService) {
		_resourcesService = resourcesService;
		_usersService     = usersService;
	}

	public async Task<IActionResult> OnGetAsync(int? id) {
		try {
			await Request.RetrieveUserAsync(_usersService);
		} catch (Exception ex) {
			Response.Redirect(Url.RouteUrl("Error", new { message = ex.Message }) ?? "/Error");
			return RedirectToPage("/Error", new { message = ex.Message });
		}

		if (id is not null) {
			Resource = await _resourcesService.GetAsync<ResourceDto>(id.Value);
		}

		return Page();
	}

	public async Task<IActionResult> OnPostAsync(int? id) {
		User user;
		try {
			user = await Request.RetrieveUserAsync(_usersService);
		} catch (Exception ex) {
			Response.Redirect(Url.RouteUrl("Error", new { message = ex.Message }) ?? "/Error");
			return RedirectToPage("/Error", new { message = ex.Message });
		}

		if (id is not null) {
			Resource.Id = id;
			await _resourcesService.UpdateAsync(Resource);
		} else {
			await _resourcesService.AddAsync(user.Id, Resource);
		}

		return RedirectToPage("/Resources/Index");
	}

	public async Task<IActionResult> OnPostDeleteAsync(int id) {
		await _resourcesService.DeleteAsync(id);
		return RedirectToPage("/Resources/Index");
	}
}
