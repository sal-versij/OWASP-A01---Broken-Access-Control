using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Undefended.Data.Models;
using Undefended.Extensions;
using Undefended.Services.Interfaces;

namespace Undefended.Pages;

public class LogoutModel : PageModel {
	public IActionResult OnGet() {
		Response.LogoutUser();
		return RedirectToPage("/Index");
	}
}
