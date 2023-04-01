using System.Linq.Expressions;
using Defended.Data.Models;
using Defended.Data.Models.Dtos;

namespace Defended.Services.Interfaces;

public interface IAdminResourcesService {
	Task<T>              GetAsync<T>(string id) where T : IExpressionProvider<T, AdminResource>;
	Task<T>              GetAsync<T>(string id, Expression<Func<AdminResource, T>> selector);
	Task<ICollection<T>> GetAllAsync<T>() where T : IExpressionProvider<T, AdminResource>;
	Task<ICollection<T>> GetAllAsync<T>(Expression<Func<AdminResource, T>> selector);
	Task<AdminResource>  AddAsync(AdminResourceDto                         resource);
	Task<AdminResource>  UpdateAsync(AdminResourceDto                      resource);
	Task                 DeleteAsync(string                                id);
}
