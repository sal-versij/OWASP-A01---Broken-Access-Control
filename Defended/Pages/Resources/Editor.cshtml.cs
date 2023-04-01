using Defended.Data.Models.Dtos;
using Defended.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Defended.Pages.Resources;

public class EditorModel : PageModel {
	private readonly IResourcesService _resourcesService;

	[BindProperty]
	public ResourceDto Resource { get; set; } = new();

	public EditorModel(IResourcesService resourcesService) {
		_resourcesService = resourcesService;
	}

	public async Task OnGetAsync(string? id) {
		if (id is not null) {
			Resource = await _resourcesService.GetAsync<ResourceDto>(id);
		}
	}

	public async Task<IActionResult> OnPostAsync(string? id) {
		if (id is not null) {
			Resource.Id = id;
			await _resourcesService.UpdateAsync(Resource);
		} else {
			await _resourcesService.AddAsync(Resource);
		}

		return RedirectToPage("/Resources/Index");
	}

	public async Task<IActionResult> OnPostDeleteAsync(string id) {
		await _resourcesService.DeleteAsync(id);
		return RedirectToPage("/Resources/Index");
	}
}
