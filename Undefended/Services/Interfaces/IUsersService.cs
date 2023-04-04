using Undefended.Data.Models;

namespace Undefended.Services.Interfaces;

public interface IUsersService {
	Task<User>              GetUserAsync(int    id);
	Task<User>              GetUserAsync(string email, string password);
	Task<ICollection<User>> GetAllUsersAsync();
	Task                    AddClaimToUserAsync(int      userId, string permission);
	Task                    RemoveClaimFromUserAsync(int userId, string permission);
}
