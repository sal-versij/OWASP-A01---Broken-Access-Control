using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Undefended.Data.Models;
using Undefended.Extensions;
using Undefended.Services.Interfaces;

namespace Undefended.Pages;

public class LoginModel : PageModel {
	private readonly IUsersService _usersService;

	[BindProperty]
	public string? Email { get; set; }

	[BindProperty]
	public string? Password { get; set; }

	public LoginModel(IUsersService usersService) {
		_usersService = usersService;
	}

	public void OnGet() { }

	public async Task<IActionResult> OnPostAsync() {
		if (Email is null || Password is null) {
			return RedirectToPage("/Error", new { message = "Email or password is null" });
		}

		User user;
		try {
			user = await _usersService.GetUserAsync(Email, GetHash(Password));
		} catch (Exception e) {
			return RedirectToPage("/Error", new { message = e.Message });
		}

		Response.LoginUser(user);

		return RedirectToPage("/Index");
	}

	public static string GetHash(string value) {
		var data     = Encoding.ASCII.GetBytes(value);
		var hashData = SHA256.HashData(data);
		return hashData.Aggregate(string.Empty, (current, b) => current + b.ToString("X2"));
	}
}
