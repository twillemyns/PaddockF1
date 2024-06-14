using System.ComponentModel.DataAnnotations;
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

    [SupplyParameterFromForm] public InputModel NewTopic { get; set; } = new();

    private IEnumerable<Topic> _topics = default!;

    private ApplicationUser? _user;

    protected override void OnInitialized()
    {
        _topics = ApplicationService.GetTopics().OrderBy(x => x.CreatedAt).Reverse();
    }

    protected override async Task OnInitializedAsync()
    {
        var userUtilities = new UserUtilities(AuthenticationStateProvider, ApplicationService);

        _user = await userUtilities.GetCurrentUser();
    }

    private void CreateTopic()
    {
        if (_user is null)
        {
            NavigationManager.NavigateTo("/Account/login");
            return;
        }

        var topic = new Topic
        {
            Title = NewTopic.Title,
            Description = NewTopic.Description,
            Author = _user
        };
        ApplicationService.AddTopic(topic);
        
        // todo: erreur pendant la redirection (essaye de rediriger alors que le forum n'est pas encore créé)
        
        NavigationManager.NavigateTo($"/forum/{topic.Id}");
    }
}

public class InputModel
{
    [Required(ErrorMessage = "Le titre est obligatoire.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Le titre doit contenir entre 3 et 50 caractères.")]
    public string Title { get; set; } = default!;

    [Required(ErrorMessage = "La description est obligatoire.")]
    [StringLength(250, MinimumLength = 5, ErrorMessage = "La description doit contenir entre 5 et 250 caractères.")]
    public string Description { get; set; } = default!;
}