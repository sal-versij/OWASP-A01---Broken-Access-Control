using System.Linq.Expressions;
using Undefended.Data.Models;
using Undefended.Data.Models.Dtos;

namespace Undefended.Services.Interfaces;

public interface IAdminResourcesService {
	Task<T>              GetAsync<T>(int              id) where T : IExpressionProvider<T, AdminResource>;
	Task<T>              GetAsync<T>(int              id, Expression<Func<AdminResource, T>> selector);
	Task<ICollection<T>> GetAllAsync<T>(int           id) where T : IExpressionProvider<T, AdminResource>;
	Task<ICollection<T>> GetAllAsync<T>(int           id, Expression<Func<AdminResource, T>> selector);
	Task<AdminResource>  AddAsync(int                 id, AdminResourceDto                   resource);
	Task<AdminResource>  UpdateAsync(AdminResourceDto resource);
	Task                 DeleteAsync(int              id);
}
