using Microsoft.AspNetCore.Components.Authorization;
using PaddockF1.Hosted.Data;
using PaddockF1.Hosted.Data.Models;

namespace PaddockF1.Hosted.Components.Shared;

public class UserUtilities(
    AuthenticationStateProvider authStateProvider,
    ApplicationService applicationService)
{
    public async Task<ApplicationUser?> GetCurrentUser()
    {
        var authenticationState = await authStateProvider.GetAuthenticationStateAsync();
        var user = authenticationState.User;
        var userId = user.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;

        return userId is null ? null : applicationService.GetUserById(userId);
    }
}