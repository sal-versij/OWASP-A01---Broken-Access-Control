using System.ComponentModel.DataAnnotations;
using Defended.Attributes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Defended.Data.Models;

public class AdminResource {
	public          string  Id          { get; set; } = Guid.NewGuid().ToString();
	public required string  UserId      { get; set; }
	public required string  Name        { get; set; }
	public          string? Description { get; set; }

	[OneToMany(OnDelete = DeleteBehavior.Cascade)]
	public IdentityUser? User { get; set; } = null;
}
