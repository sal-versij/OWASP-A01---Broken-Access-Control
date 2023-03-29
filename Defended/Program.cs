using Defended.Extensions;
using Defended.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services
	   .AddApplicationDbContext(connectionString)
	   .AddApplicationIdentity()
	   .AddInjectablesFromAssembly();

builder.Services.AddAuthorization(Policies.AddPolicies);

builder.Services.AddRazorPages(
	options => {
		options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage", nameof(Policies.LoggedPolicy));
		options.Conventions.AuthorizeFolder("/", nameof(Policies.LoggedPolicy));
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.UseMigrationsEndPoint();
} else {
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors();
app.UseAuthorization();

app.MapRazorPages();
app.MapEndPoints();

app.Run();
