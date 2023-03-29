using System.Linq.Expressions;
using Defended.Attributes;
using Defended.Data;
using Defended.Data.Models;
using Defended.EndPoints.Models;
using Defended.Exceptions;
using Defended.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Defended.Services;

[Injectable<IResourcesService>]
public class ResourcesService : IResourcesService {
	private readonly ApplicationDbContext _dbContext;
	private readonly IPermissionService   _permissionService;

	public ResourcesService(ApplicationDbContext dbContext, IPermissionService permissionService) {
		_dbContext         = dbContext;
		_permissionService = permissionService;
	}

	public async Task<T> GetAsync<T>(string id, Expression<Func<Resource, T>> selector) {
		await _permissionService.AssertUserCanAccessResourceAsync(id);

		var resource = await _dbContext.Resources.Where(x => x.Id == id).Select(selector).SingleOrDefaultAsync();
		if (resource is null) {
			throw new NotFoundException("Resource not found");
		}

		return resource;
	}

	public async Task<ICollection<T>> GetAllAsync<T>(Expression<Func<Resource, T>> selector) {
		var userId = _permissionService.GetUserId() ?? throw new("User not logged");
		return await _dbContext.Resources.Where(x => x.UserId == userId).Select(selector).ToListAsync();
	}

	public async Task<Resource> AddAsync(ResourceDto resource) {
		if (resource.Id is not null)
			throw new("Resource Id shouldn't be provided");

		var userId = _permissionService.GetUserId() ?? throw new("User not logged");

		var newResource = new Resource {
			Name        = resource.Name,
			Description = resource.Description,
			UserId      = userId,
		};

		await _dbContext.Resources.AddAsync(newResource);
		await _dbContext.SaveChangesAsync();
		return newResource;
	}

	public async Task<Resource> UpdateAsync(ResourceDto resource) {
		if (resource.Id is null)
			throw new("Resource Id is null");

		await _permissionService.AssertUserCanAccessResourceAsync(resource.Id);

		var resourceToUpdate = await _dbContext.Resources.FindAsync(resource.Id);
		if (resourceToUpdate is null)
			throw new("Resource not found");

		resourceToUpdate.Name        = resource.Name;
		resourceToUpdate.Description = resource.Description;
		await _dbContext.SaveChangesAsync();
		return resourceToUpdate;
	}

	public async Task DeleteAsync(string id) {
		await _permissionService.AssertUserCanAccessResourceAsync(id);

		var resource = await _dbContext.Resources.FindAsync(id);
		if (resource is null)
			throw new("Resource not found");

		_dbContext.Resources.Remove(resource);
		await _dbContext.SaveChangesAsync();
	}
}
