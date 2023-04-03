using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Undefended.Data.Models;
using Undefended.Extensions;

namespace Undefended.Data;

public class ApplicationDbContext : DbContext {
	public DbSet<Resource>       Resources       { get; set; }
	public DbSet<AdminResource>  AdminResources  { get; set; }
	public DbSet<User>           Users           { get; set; }
	public DbSet<UserPermission> UserPermissions { get; set; }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);

		modelBuilder.ConfigureRelationships();
	}
}
