using Microsoft.AspNetCore.Components;
using PaddockF1.Module.Forum.Data;
using PaddockF1.Module.Forum.Models;

namespace PaddockF1.Module.Forum.Components.Pages;

public partial class TopicDetail : ComponentBase
{
    [Inject] private ForumService ForumService { get; set; } = default!;
    
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Parameter] public string Id { get; set; } = default!;

    private Topic _topic = default!;
    
    protected override void OnInitialized()
    {
        _topic = ForumService.GetTopicById(Guid.Parse(Id));
        
        if (_topic == null)
        {
            // todo: create a NotFound component
            NavigationManager.NavigateTo("/NotFound");
        }
        else
        {
            _topic.Messages = ForumService.GetMessagesByTopicId(_topic.Id).ToList();
        }
    }
}