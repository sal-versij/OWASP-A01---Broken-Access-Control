using Defended.Data.Models.Dtos;
using Defended.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Defended.Pages.Admins.Resources;

public class EditorModel : PageModel {
	private readonly IAdminResourcesService _resourcesService;

	[BindProperty]
	public AdminResourceDto Resource { get; set; } = new();

	public EditorModel(IAdminResourcesService resourcesService) {
		_resourcesService = resourcesService;
	}

	public async Task OnGetAsync(string? id) {
		if (id is not null) {
			Resource = await _resourcesService.GetAsync<AdminResourceDto>(id);
		}
	}

	public async Task<IActionResult> OnPostAsync(string? id) {
		if (id is not null) {
			Resource.Id = id;
			await _resourcesService.UpdateAsync(Resource);
		} else {
			await _resourcesService.AddAsync(Resource);
		}

		return RedirectToPage("/Admins/Resources/Index");
	}

	public async Task<IActionResult> OnPostDeleteAsync(string id) {
		await _resourcesService.DeleteAsync(id);
		return RedirectToPage("/Admins/Resources/Index");
	}
}
