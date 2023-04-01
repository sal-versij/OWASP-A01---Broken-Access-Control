using Defended.Data.Models.Dtos;
using Defended.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Defended.Pages.Admins.Resources;

public class IndexModel : PageModel {
	private readonly IAdminResourcesService _resourcesService;

	public ICollection<AdminResourceDto>? Resources { get; set; }

	public IndexModel(IAdminResourcesService resourcesService) {
		_resourcesService = resourcesService;
	}

	public async Task OnGetAsync() {
		Resources = await _resourcesService.GetAllAsync<AdminResourceDto>();
	}
}
