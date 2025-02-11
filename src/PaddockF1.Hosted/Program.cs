using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PaddockF1.Hosted.Components;
using PaddockF1.Hosted.Components.Account;
using PaddockF1.Hosted.Data;
using PaddockF1.Hosted.Data.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

#region authentification

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var keyVaultUri = "https://dbpwd.vault.azure.net";

    var client = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());

    var secret = client.GetSecret("DbPassword");
    
    if (builder.Environment.IsDevelopment())
    {
        var connectionString = builder.Configuration.GetConnectionString("Development") ??
                               throw new InvalidOperationException("Connection string 'Development' not found.");
        options.UseMySql(connectionString + secret.Value.Value, new MySqlServerVersion(new Version(8, 0, 41)));
    }
    else if (builder.Environment.IsProduction())
    {
        var connectionString = builder.Configuration.GetConnectionString("Production") ??
                               throw new InvalidOperationException("Connection string 'Production' not found.");
        options.UseMySql(connectionString + secret.Value.Value, new MySqlServerVersion(new Version(8, 0)));
    }
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, EmailSender>();

builder.Services.AddScoped<ApplicationService>(provider =>
{
    var context = provider.GetRequiredService<ApplicationDbContext>();

    return new ApplicationService(context);
});

#endregion

builder.Services.AddQuickGridEntityFrameworkAdapter();

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
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await DbInitializer.InitializeRolesAsync(roleManager);
    
    var appDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    await DbInitializer.InitializeDataAsync(appDbContext, userManager);
}

#endif

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(
        typeof(PaddockF1.Hosted.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();