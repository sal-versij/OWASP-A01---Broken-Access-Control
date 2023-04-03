using System.Linq.Expressions;
using Undefended.Services.Interfaces;

namespace Undefended.Data.Models.Dtos;

public class AdminResourceDto : IExpressionProvider<AdminResourceDto, AdminResource> {
	public static Expression<Func<AdminResource, AdminResourceDto>> Expression => resource => new(resource);

	public int? Id          { get; set; }
	public string  Name        { get; set; } = null!;
	public string? Description { get; set; }

	public AdminResourceDto() { }


	public AdminResourceDto(AdminResource resource) {
		Id          = resource.Id;
		Name        = resource.Name;
		Description = resource.Description;
	}
}
