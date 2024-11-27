using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PaddockF1.Hosted.Components.Shared;
using PaddockF1.Hosted.Data;
using PaddockF1.Hosted.Data.Models;

namespace PaddockF1.Hosted.Components.Forum.Pages;

public partial class TopicDetail : ComponentBase
{
    [Inject] private ApplicationService ApplicationService { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

    [Parameter] public string Id { get; set; } = default!;

    [SupplyParameterFromForm] private InputModel Model { get; set; } = new();

    private Topic? _topic;

    private ApplicationUser? _user;

    protected override void OnInitialized()
    {
        _topic = ApplicationService.GetTopicById(Guid.Parse(Id));

        if (_topic == null)
        {
            // todo: create a NotFound component
            NavigationManager.NavigateTo("/NotFound");
        }
        else
        {
            _topic.Messages = ApplicationService.GetMessagesByTopicId(_topic.Id).ToList();
        }
    }

    protected override async Task OnInitializedAsync()
    {
        var userUtilities = new UserUtilities(AuthenticationStateProvider, ApplicationService);

        _user = await userUtilities.GetCurrentUser();
    }

    private void Callback()
    {
        if (_user is null) return;

        var message = new Message
        {
            TopicId = _topic!.Id,
            User = _user,
            Content = Model.Content,
        };
        ApplicationService.AddMessage(message);
        
        Model.Content = string.Empty;
    }

    private sealed class InputModel
    {
        public string Content { get; set; } = string.Empty;
    }
}