using Microsoft.AspNetCore.Components;
using PaddockF1.Module.Forum.Data;
using PaddockF1.Module.Forum.Models;

namespace PaddockF1.Module.Forum.Components.Pages;

public partial class TopicList : ComponentBase
{
    [Inject] private ForumService ForumService { get; set; } = default!;
    
    private List<Topic> _topics = default!;
    
    protected override void OnInitialized()
    {
        _topics = ForumService.GetTopics().ToList();
    }
}