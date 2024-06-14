using Microsoft.AspNetCore.Components;
using PaddockF1.Hosted.Data;
using PaddockF1.Hosted.Data.Models;

namespace PaddockF1.Hosted.Components.Pages;

public partial class Home
{
    [Inject] private ApplicationService ApplicationService { get; set; } = default!;
    
    private IEnumerable<Topic> Topics { get; set; } = default!;

    protected override void OnInitialized()
    {
        Topics = ApplicationService.GetTopics().OrderBy(x => x.CreatedAt).Reverse().Take(3);
    }
}