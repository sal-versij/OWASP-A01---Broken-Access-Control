using System.Linq.Expressions;
using Undefended.Services.Interfaces;

namespace Undefended.Data.Models.Dtos;

public class ResourceDto : IExpressionProvider<ResourceDto, Resource> {
	public static Expression<Func<Resource, ResourceDto>> Expression => resource => new(resource);

	public int?    Id          { get; set; }
	public string  Name        { get; set; } = null!;
	public string? Description { get; set; }

	public ResourceDto() { }


	public ResourceDto(Resource resource) {
		Id          = resource.Id;
		Name        = resource.Name;
		Description = resource.Description;
	}
}
