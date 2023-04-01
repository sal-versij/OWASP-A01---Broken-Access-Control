using Defended.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Defended.Data;

public class ApplicationDbContext : IdentityDbContext {
	public DbSet<Resource>      Resources      { get; set; }
	public DbSet<AdminResource> AdminResources { get; set; }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options) { }
}
