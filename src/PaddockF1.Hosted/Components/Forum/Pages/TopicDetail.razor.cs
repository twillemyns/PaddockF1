using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PaddockF1.Hosted.Components.Account;
using PaddockF1.Hosted.Data;
using PaddockF1.Hosted.Data.Models;

namespace PaddockF1.Hosted.Components.Forum.Pages;

public partial class TopicDetail : ComponentBase
{
    [Inject] private ApplicationService ApplicationService { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private IdentityUserAccessor UserAccessor { get; set; } = default!;

    [CascadingParameter] private HttpContext HttpContext { get; set; } = default!;

    [Parameter] public string Id { get; set; } = default!;
    
    [SupplyParameterFromForm] private InputModel Model { get; set; } = new();

    private Topic? _topic;
    
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

    private async Task Callback()
    {
        var message = new Message
        {
            TopicId = _topic!.Id,
            User = await UserAccessor.GetRequiredUserAsync(HttpContext),
            Content = Model.Content,
        };
        ApplicationService.AddMessage(message);

        StateHasChanged();
    }

    private sealed class InputModel
    {
        public string Content { get; set; } = string.Empty;
    }
}

