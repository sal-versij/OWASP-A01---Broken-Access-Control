using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Undefended.Data.Models;

public class User {
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public required string Email    { get; set; }
	public required string Password { get; set; }

	public ICollection<UserPermission>? Permissions { get; set; }
}
