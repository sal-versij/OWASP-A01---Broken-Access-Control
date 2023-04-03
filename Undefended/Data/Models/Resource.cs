using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Undefended.Attributes;

namespace Undefended.Data.Models;

public class Resource {
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public required int     UserId      { get; set; }
	public required string  Name        { get; set; }
	public          string? Description { get; set; }

	[OneToMany(OnDelete = DeleteBehavior.Cascade)]
	public User? User { get; set; }
}
