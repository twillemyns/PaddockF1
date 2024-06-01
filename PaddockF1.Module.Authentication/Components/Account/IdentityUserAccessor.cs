using Microsoft.AspNetCore.Http;

namespace PaddockF1.Module.Authentication.Components.Account;

internal sealed class IdentityUserAccessor(UserManager<ApplicationUser> userManager, IdentityRedirectManager redirectManager)
{
    public async Task<ApplicationUser> GetRequiredUserAsync(HttpContext context)
    {
        var user = await userManager.GetUserAsync(context.User);

        if (user is null)
        {
            redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Erreur: Impossible de charger un utilisateur avec l'ID: '{userManager.GetUserId(context.User)}'.", context);
        }

        return user;
    }
}