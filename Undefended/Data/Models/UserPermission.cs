using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Undefended.Attributes;

namespace Undefended.Data.Models;

[PrimaryKey(nameof(UserId), nameof(Permission))]
public class UserPermission {
	public required int UserId { get; set; }

	public required string Permission { get; set; }

	[OneToMany(OnDelete = DeleteBehavior.Cascade)]
	public User? User { get; set; }
}
