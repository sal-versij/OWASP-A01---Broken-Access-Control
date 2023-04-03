using Microsoft.EntityFrameworkCore;
using Undefended.Attributes;
using Undefended.Data;
using Undefended.Data.Models;
using Undefended.Services.Interfaces;

namespace Undefended.Services;

[Injectable<IUsersService>]
public class UsersService : IUsersService {
	private readonly ApplicationDbContext _dbContext;

	public UsersService(ApplicationDbContext dbContext) {
		_dbContext = dbContext;
	}

	public async Task<User> GetUserAsync(int id) {
		return await _dbContext.Users.Include(x => x.Permissions).FirstOrDefaultAsync(u => u.Id == id) ?? throw new("User not found");
	}

	public async Task<User> GetUserAsync(string email, string password) {
		return await _dbContext.Users.Include(x => x.Permissions).FirstOrDefaultAsync(u => u.Email == email && u.Password == password) ?? throw new("User not found");
	}

	public async Task<ICollection<User>> GetAllUsersAsync() {
		return await _dbContext.Users.Include(x => x.Permissions).ToListAsync();
	}
}
