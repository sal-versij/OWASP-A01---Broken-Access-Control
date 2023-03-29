using System.Linq.Expressions;
using Defended.Data.Models;

namespace Defended.EndPoints.Models;

public class ResourceDto {
	public static readonly Expression<Func<Resource, ResourceDto>> Expression = resource => new(resource);

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
