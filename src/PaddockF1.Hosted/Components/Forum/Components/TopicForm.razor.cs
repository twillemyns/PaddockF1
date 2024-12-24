using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PaddockF1.Hosted.Components.Shared;
using PaddockF1.Hosted.Data;
using PaddockF1.Hosted.Data.Models;

namespace PaddockF1.Hosted.Components.Forum.Components;

public partial class TopicForm
{
    [Parameter] public Topic? InputModel { get; set; }

    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

    [Inject] private ApplicationService ApplicationService { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    
    private ApplicationUser? _user;

    private Guid? _topicId;

    protected override async Task OnInitializedAsync()
    {
        var userUtilities = new UserUtilities(AuthenticationStateProvider, ApplicationService);

        _user = await userUtilities.GetCurrentUser();
    }

    protected override void OnInitialized()
    {
        if (InputModel is null)
        {
            InputModel ??= new Topic();
        }
        else
        {
            _topicId = InputModel.Id;
        }
    }

    private void ValidSubmitAction()
    {
        if (_user is null)
        {
            NavigationManager.NavigateTo("/Account/login");
            return;
        }

        if (_topicId is not null)
        {
            var existingTopic = ApplicationService.GetTopicById(_topicId.Value);
            if (existingTopic is null) return;
            
            existingTopic.Title = InputModel!.Title;
            existingTopic.Description = InputModel!.Description;
            ApplicationService.SaveChanges();
        }
        else
        {
            InputModel!.Author = _user;
            ApplicationService.AddTopic(InputModel!);
        }

        NavigationManager.Refresh(true);
    }
}

// public class InputModel
// {
//     [Required(ErrorMessage = "Le titre est obligatoire.")]
//     [StringLength(50, MinimumLength = 3, ErrorMessage = "Le titre doit contenir entre 3 et 50 caractères.")]
//     public string Title { get; set; } = default!;
//
//     [Required(ErrorMessage = "La description est obligatoire.")]
//     [StringLength(500, MinimumLength = 5, ErrorMessage = "La description doit contenir entre 5 et 500 caractères.")]
//     public string Description { get; set; } = default!;
// }