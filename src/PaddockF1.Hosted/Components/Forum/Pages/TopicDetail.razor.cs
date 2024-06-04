using Microsoft.AspNetCore.Components;
using PaddockF1.Hosted.Data;
using PaddockF1.Hosted.Data.Models;

namespace PaddockF1.Hosted.Components.Forum.Pages;

public partial class TopicDetail : ComponentBase
{
    [Inject] private ApplicationService ApplicationService { get; set; } = default!;
    
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Parameter] public string Id { get; set; } = default!;

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
}