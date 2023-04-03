using System.Linq.Expressions;
using Undefended.Data.Models;
using Undefended.Data.Models.Dtos;

namespace Undefended.Services.Interfaces;

public interface IResourcesService {
	Task<T>              GetAsync<T>(int         id) where T : IExpressionProvider<T, Resource>;
	Task<T>              GetAsync<T>(int         id, Expression<Func<Resource, T>> selector);
	Task<ICollection<T>> GetAllAsync<T>(int      userId) where T : IExpressionProvider<T, Resource>;
	Task<ICollection<T>> GetAllAsync<T>(int      userId, Expression<Func<Resource, T>> selector);
	Task<Resource>       AddAsync(int            userId, ResourceDto                   resource);
	Task<Resource>       UpdateAsync(ResourceDto resource);
	Task                 DeleteAsync(int         id);
}
