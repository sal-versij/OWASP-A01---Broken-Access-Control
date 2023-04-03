using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Undefended.Attributes;
using Undefended.Data;
using Undefended.Data.Models;
using Undefended.Data.Models.Dtos;
using Undefended.Exceptions;
using Undefended.Services.Interfaces;

namespace Undefended.Services;

[Injectable<IResourcesService>]
public class ResourcesService : IResourcesService {
	private readonly ApplicationDbContext _dbContext;

	public ResourcesService(ApplicationDbContext dbContext) {
		_dbContext = dbContext;
	}

	public Task<T> GetAsync<T>(int id) where T : IExpressionProvider<T, Resource> {
		return GetAsync(id, T.Expression);
	}

	public async Task<T> GetAsync<T>(int id, Expression<Func<Resource, T>> selector) {
		var resource = await _dbContext.Resources.Where(x => x.Id == id).Select(selector).SingleOrDefaultAsync();
		if (resource is null) {
			throw new NotFoundException("Resource not found");
		}

		return resource;
	}

	public async Task<ICollection<T>> GetAllAsync<T>(int userId) where T : IExpressionProvider<T, Resource> {
		return await GetAllAsync(userId, T.Expression);
	}

	public async Task<ICollection<T>> GetAllAsync<T>(int userId, Expression<Func<Resource, T>> selector) {
		return await _dbContext.Resources.Where(x => x.UserId == userId).Select(selector).ToListAsync();
	}

	public async Task<Resource> AddAsync(int userId, ResourceDto resource) {
		if (resource.Id is not null)
			throw new("Resource Id shouldn't be provided");

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

		var resourceToUpdate = await _dbContext.Resources.FindAsync(resource.Id);
		if (resourceToUpdate is null)
			throw new("Resource not found");

		resourceToUpdate.Name        = resource.Name;
		resourceToUpdate.Description = resource.Description;
		await _dbContext.SaveChangesAsync();
		return resourceToUpdate;
	}

	public async Task DeleteAsync(int id) {
		var resource = await _dbContext.Resources.FindAsync(id);
		if (resource is null)
			throw new("Resource not found");

		_dbContext.Resources.Remove(resource);
		await _dbContext.SaveChangesAsync();
	}
}
