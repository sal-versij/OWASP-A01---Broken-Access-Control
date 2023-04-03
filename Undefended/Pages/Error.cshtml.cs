using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Undefended.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel : PageModel {
	public string? RequestId     { get; set; }
	public string? Message       { get; set; }
	public bool    ShowRequestId => !string.IsNullOrEmpty(RequestId);

	public void OnGet(string? message) {
		RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
		Message   = message;
	}
}
