using PaddockF1.Hosted.Components;
using PaddockF1.Hosted.Data;
using PaddockF1.Module.Authentication;
using PaddockF1.Module.Authentication.Data;
using PaddockF1.Module.Forum;
using PaddockF1.Module.Forum.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var connectionString = builder.Configuration.GetConnectionString("SQLite") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddModuleAuthentication(connectionString);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddForum(connectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

#if DEBUG

using (var scope = app.Services.CreateScope())
{
    var appDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var forumDbContext = scope.ServiceProvider.GetRequiredService<ForumContext>();
    DbInitializer.Initialize(appDbContext, forumDbContext);
}

#endif

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(
        typeof(PaddockF1.Hosted.Client._Imports).Assembly,
        typeof(PaddockF1.Module.Forum.Components._Imports).Assembly,
        typeof(PaddockF1.Module.Authentication.Components.Account.Pages._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
