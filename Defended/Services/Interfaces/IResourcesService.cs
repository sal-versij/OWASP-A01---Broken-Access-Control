using System.Linq.Expressions;
using Defended.Data.Models;
using Defended.EndPoints.Models;

namespace Defended.Services.Interfaces;

public interface IResourcesService {
	Task<T>              GetAsync<T>(string                           id, Expression<Func<Resource, T>> selector);
	Task<ICollection<T>> GetAllAsync<T>(Expression<Func<Resource, T>> selector);
	Task<Resource>       AddAsync(ResourceDto                         resource);
	Task<Resource>       UpdateAsync(ResourceDto                      resource);
	Task                 DeleteAsync(string                           id);
}
