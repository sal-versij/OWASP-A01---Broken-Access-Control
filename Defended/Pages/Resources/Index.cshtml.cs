using Defended.Data.Models;
using Defended.Data.Models.Dtos;
using Defended.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Defended.Pages.Resources;

public class IndexModel : PageModel {
	private readonly IResourcesService _resourcesService;

	public ICollection<ResourceDto>? Resources { get; set; }

	public IndexModel(IResourcesService resourcesService) {
		_resourcesService = resourcesService;
	}

	public async Task OnGetAsync() {
		Resources = await _resourcesService.GetAllAsync<ResourceDto>();
	}
}
