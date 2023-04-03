using Microsoft.EntityFrameworkCore;

namespace Undefended.Extensions;

public static class DbContextExtensions {
	public static Task EnableIdentityInsertAsync<T>(this DbContext context) {
		return SetIdentityInsert<T>(context, true);
	}

	public static Task DisableIdentityInsertAsync<T>(this DbContext context) {
		return SetIdentityInsert<T>(context, false);
	}

	public static async Task SaveWithIdentityInsert<T>(this DbContext dbContext) {
		await dbContext.EnableIdentityInsertAsync<T>();
		await dbContext.SaveChangesAsync();
		await dbContext.DisableIdentityInsertAsync<T>();
	}

	private static Task SetIdentityInsert<T>(DbContext context, bool enable) {
		var entityType = context.Model.FindEntityType(typeof(T));
		if (entityType == null)
			throw new();

		var value = enable ? "ON" : "OFF";
		return context.Database.ExecuteSqlRawAsync(
			$"SET IDENTITY_INSERT {entityType.GetSchema() ?? "[dbo]"}.{entityType.GetTableName()} {value}");
	}
}
