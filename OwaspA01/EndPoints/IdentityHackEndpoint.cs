using OwaspA01.Services.Interfaces;

namespace OwaspA01.EndPoints;

public class IdentityHackEndpoint : IEndpoint {
	public void MapEndPoints(WebApplication app) {
		app.MapGet("/Identity/Account/Register", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login",  true, true)));
		app.MapPost("/Identity/Account/Register", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true, true)));
	}
}
