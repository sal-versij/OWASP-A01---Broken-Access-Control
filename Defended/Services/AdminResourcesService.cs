using System.Linq.Expressions;
using Defended.Attributes;
using Defended.Data;
using Defended.Data.Models;
using Defended.Data.Models.Dtos;
using Defended.Exceptions;
using Defended.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Defended.Services;

[Injectable<IAdminResourcesService>]
public class AdminResourcesService : IAdminResourcesService {
	private readonly ApplicationDbContext _dbContext;
	private readonly IPermissionService   _permissionService;

	public AdminResourcesService(ApplicationDbContext dbContext, IPermissionService permissionService) {
		_dbContext         = dbContext;
		_permissionService = permissionService;
	}

	public Task<T> GetAsync<T>(string id) where T : IExpressionProvider<T, AdminResource> {
		return GetAsync(id, T.Expression);
	}

	public async Task<T> GetAsync<T>(string id, Expression<Func<AdminResource, T>> selector) {
		await _permissionService.AssertUserIsAdminAsync();
		await _permissionService.AssertUserCanAccessAdminResourceAsync(id);

		var resource = await _dbContext.AdminResources.Where(x => x.Id == id).Select(selector).SingleOrDefaultAsync();
		if (resource is null) {
			throw new NotFoundException("Resource not found");
		}

		return resource;
	}

	public async Task<ICollection<T>> GetAllAsync<T>() where T : IExpressionProvider<T, AdminResource> {
		return await GetAllAsync(T.Expression);
	}

	public async Task<ICollection<T>> GetAllAsync<T>(Expression<Func<AdminResource, T>> selector) {
		await _permissionService.AssertUserIsAdminAsync();

		var userId = _permissionService.GetUserId() ?? throw new("User not logged");
		return await _dbContext.AdminResources.Where(x => x.UserId == userId).Select(selector).ToListAsync();
	}

	public async Task<AdminResource> AddAsync(AdminResourceDto resource) {
		if (resource.Id is not null)
			throw new("Resource Id shouldn't be provided");

		await _permissionService.AssertUserIsAdminAsync();

		var userId = _permissionService.GetUserId() ?? throw new("User not logged");

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

		await _permissionService.AssertUserIsAdminAsync();
		await _permissionService.AssertUserCanAccessAdminResourceAsync(resource.Id);

		var resourceToUpdate = await _dbContext.AdminResources.FindAsync(resource.Id);
		if (resourceToUpdate is null)
			throw new("Resource not found");

		resourceToUpdate.Name        = resource.Name;
		resourceToUpdate.Description = resource.Description;
		await _dbContext.SaveChangesAsync();
		return resourceToUpdate;
	}

	public async Task DeleteAsync(string id) {
		await _permissionService.AssertUserIsAdminAsync();
		await _permissionService.AssertUserCanAccessAdminResourceAsync(id);

		var resource = await _dbContext.AdminResources.FindAsync(id);
		if (resource is null)
			throw new("Resource not found");

		_dbContext.AdminResources.Remove(resource);
		await _dbContext.SaveChangesAsync();
	}
}
