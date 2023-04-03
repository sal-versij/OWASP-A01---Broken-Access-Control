using Undefended.Data.Models.Dtos;
using Undefended.Exceptions;
using Undefended.Extensions;
using Undefended.Services.Interfaces;
using Undefended.Utils;

namespace Undefended.EndPoints;

public class ResourcesEndpoint : IEndpoint {
	private static async Task<IResult> RetrieveResource(int id, IResourcesService service) {
		try {
			var resource = await service.GetAsync<ResourceDto>(id);
			return Results.Ok(resource);
		} catch (NotFoundException ex) {
			return Results.NotFound(ex.Message);
		} catch (UserNotLoggedInException) {
			return Results.Unauthorized();
		} catch (ForbiddenException ex) {
			return Results.BadRequest(ex.Message);
		} catch (Exception ex) {
			return Results.BadRequest(ex.Message);
		}
	}

	private static async Task<IResult> RetrieveResources(HttpRequest request, IUsersService usersService, IResourcesService service) {
		try {
			var user = await request.RetrieveUserAsync(usersService);

			var resources = await service.GetAllAsync<ResourceDto>(user.Id);
			return Results.Ok(resources);
		} catch (NotFoundException ex) {
			return Results.NotFound(ex.Message);
		} catch (UserNotLoggedInException) {
			return Results.Unauthorized();
		} catch (ForbiddenException ex) {
			return Results.BadRequest(ex.Message);
		} catch (Exception ex) {
			return Results.BadRequest(ex.Message);
		}
	}

	private static async Task<IResult> AddResource(HttpRequest request, ResourceDto resource, IUsersService usersService, IResourcesService service) {
		try {
			var user = await request.RetrieveUserAsync(usersService);

			await service.AddAsync(user.Id, resource);
			return Results.Ok("Resource added successfully!");
		} catch (NotFoundException ex) {
			return Results.NotFound(ex.Message);
		} catch (UserNotLoggedInException) {
			return Results.Unauthorized();
		} catch (ForbiddenException ex) {
			return Results.BadRequest(ex.Message);
		} catch (Exception ex) {
			return Results.BadRequest(ex.Message);
		}
	}

	private static async Task<IResult> UpdateResource(ResourceDto resource, IResourcesService service) {
		try {
			await service.UpdateAsync(resource);
			return Results.Ok("Resource updated successfully!");
		} catch (NotFoundException ex) {
			return Results.NotFound(ex.Message);
		} catch (UserNotLoggedInException) {
			return Results.Unauthorized();
		} catch (ForbiddenException ex) {
			return Results.BadRequest(ex.Message);
		} catch (Exception ex) {
			return Results.BadRequest(ex.Message);
		}
	}

	private static async Task<IResult> DeleteResource(int id, IResourcesService service) {
		try {
			await service.DeleteAsync(id);
			return Results.Ok("Resource deleted successfully!");
		} catch (NotFoundException ex) {
			return Results.NotFound(ex.Message);
		} catch (UserNotLoggedInException) {
			return Results.Unauthorized();
		} catch (ForbiddenException ex) {
			return Results.BadRequest(ex.Message);
		} catch (Exception ex) {
			return Results.BadRequest(ex.Message);
		}
	}

	public void MapEndPoints(WebApplication app) {
		app.MapGet("/api/resource/{id}", RetrieveResource).RequireAuthorization(nameof(Policies.LoggedPolicy));
		app.MapGet("/api/resources",     RetrieveResources).RequireAuthorization(nameof(Policies.LoggedPolicy));
		app.MapPost("/api/resource", AddResource).RequireAuthorization(nameof(Policies.LoggedPolicy));
		app.MapPut("/api/resource", UpdateResource).RequireAuthorization(nameof(Policies.LoggedPolicy));
		app.MapDelete("/api/resource/{id}", DeleteResource).RequireAuthorization(nameof(Policies.LoggedPolicy));
	}
}
