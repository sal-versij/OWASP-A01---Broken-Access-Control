using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Undefended.Attributes;
using Undefended.Data;
using Undefended.Data.Models;
using Undefended.Data.Models.Dtos;
using Undefended.Exceptions;
using Undefended.Services.Interfaces;

namespace Undefended.Services;

[Injectable<IAdminResourcesService>]
public class AdminResourcesService : IAdminResourcesService {
	private readonly ApplicationDbContext _dbContext;

	public AdminResourcesService(ApplicationDbContext dbContext) {
		_dbContext = dbContext;
	}

	public Task<T> GetAsync<T>(int id) where T : IExpressionProvider<T, AdminResource> {
		return GetAsync(id, T.Expression);
	}

	public async Task<T> GetAsync<T>(int id, Expression<Func<AdminResource, T>> selector) {
		var resource = await _dbContext.AdminResources.Where(x => x.Id == id).Select(selector).SingleOrDefaultAsync();
		if (resource is null) {
			throw new NotFoundException("Resource not found");
		}

		return resource;
	}

	public async Task<ICollection<T>> GetAllAsync<T>(int userId) where T : IExpressionProvider<T, AdminResource> {
		return await GetAllAsync(userId, T.Expression);
	}

	public async Task<ICollection<T>> GetAllAsync<T>(int userId, Expression<Func<AdminResource, T>> selector) {
		return await _dbContext.AdminResources.Where(x => x.UserId == userId).Select(selector).ToListAsync();
	}

	public async Task<AdminResource> AddAsync(int userId, AdminResourceDto resource) {
		if (resource.Id is not null)
			throw new("Resource Id shouldn't be provided");

		var newResource = new AdminResource {
			Name        = resource.Name,
			Description = resource.Description,
			UserId      = userId,
		};

		await _dbContext.AdminResources.AddAsync(newResource);
		await _dbContext.SaveChangesAsync();
		return newResource;
	}

	public async Task<AdminResource> UpdateAsync(AdminResourceDto resource) {
		if (resource.Id is null)
			throw new("Resource Id is null");

		var resourceToUpdate = await _dbContext.AdminResources.FindAsync(resource.Id);
		if (resourceToUpdate is null)
			throw new("Resource not found");

		resourceToUpdate.Name        = resource.Name;
		resourceToUpdate.Description = resource.Description;
		await _dbContext.SaveChangesAsync();
		return resourceToUpdate;
	}

	public async Task DeleteAsync(int id) {
		var resource = await _dbContext.AdminResources.FindAsync(id);
		if (resource is null)
			throw new("Resource not found");

		_dbContext.AdminResources.Remove(resource);
		await _dbContext.SaveChangesAsync();
	}
}
