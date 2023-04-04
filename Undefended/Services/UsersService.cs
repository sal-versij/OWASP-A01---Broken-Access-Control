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

	public async Task AddClaimToUserAsync(int userId, string permission) {
		var user = await GetUserAsync(userId);

		if (user.Permissions?.Any(x => x.Permission == permission) == true) {
			throw new("User already has this permission");
		}

		user.Permissions!.Add(
			new() {
				Permission = permission,
				UserId     = userId,
			});

		await _dbContext.SaveChangesAsync();
	}

	public async Task RemoveClaimFromUserAsync(int userId, string permission) {
		var user = await GetUserAsync(userId);

		var userPermission = user.Permissions?.FirstOrDefault(x => x.Permission == permission);

		if (userPermission == null) {
			throw new("User does not have this permission");
		}

		user.Permissions!.Remove(userPermission);

		await _dbContext.SaveChangesAsync();
	}
}
