using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PaddockF1.Hosted.Components.Shared;
using PaddockF1.Hosted.Data;
using PaddockF1.Hosted.Data.Models;

namespace PaddockF1.Hosted.Components.Forum.Pages;

public partial class TopicList : ComponentBase
{
    [Inject] private ApplicationService ApplicationService { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

    private IEnumerable<Topic> _topics = default!;

    private ApplicationUser? _user;

    protected override void OnInitialized() =>
        _topics = ApplicationService.GetTopics().OrderBy(x => x.CreatedAt).Reverse();

    protected override async Task OnInitializedAsync()
    {
        var userUtilities = new UserUtilities(AuthenticationStateProvider, ApplicationService);

        _user = await userUtilities.GetCurrentUser();
    }
}