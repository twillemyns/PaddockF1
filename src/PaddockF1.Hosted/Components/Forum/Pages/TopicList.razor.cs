using Microsoft.AspNetCore.Components;
using PaddockF1.Hosted.Data;
using PaddockF1.Hosted.Data.Models;

namespace PaddockF1.Hosted.Components.Forum.Pages;

public partial class TopicList : ComponentBase
{
    [Inject] private ApplicationService ApplicationService { get; set; } = default!;
    
    private List<Topic> _topics = default!;
    
    protected override void OnInitialized()
    {
        _topics = ApplicationService.GetTopics().ToList();
    }
}