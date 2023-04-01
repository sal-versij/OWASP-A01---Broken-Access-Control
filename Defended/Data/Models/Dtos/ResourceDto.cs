using System.Linq.Expressions;
using Defended.Services.Interfaces;

namespace Defended.Data.Models.Dtos;

public class ResourceDto : IExpressionProvider<ResourceDto, Resource> {
	public static Expression<Func<Resource, ResourceDto>> Expression => resource => new(resource);

	public string? Id          { get; set; }
	public string  Name        { get; set; } = null!;
	public string? Description { get; set; }

	public ResourceDto() { }


	public ResourceDto(Resource resource) {
		Id          = resource.Id;
		Name        = resource.Name;
		Description = resource.Description;
	}
}
