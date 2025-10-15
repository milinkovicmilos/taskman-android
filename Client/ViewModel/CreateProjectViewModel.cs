using Client.Model;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModel;

public partial class CreateProjectViewModel : BaseViewModel
{
    [ObservableProperty] private string _name;
    [ObservableProperty] private string _description;
    [ObservableProperty] private string _errorMessage;

    private readonly ProjectsService _service;

    public CreateProjectViewModel(ProjectsService service)
    {
        _service = service;
    }

    [RelayCommand]
    private async Task CreateProject()
    {
        // Clear errors
        ErrorMessage = string.Empty;

        if (string.IsNullOrEmpty(Name))
        {
            ErrorMessage = "Project name cannot be empty.";
            return;
        }

        if (string.IsNullOrEmpty(Description))
        {
            ErrorMessage = "Project description cannot be empty.";
            return;
        }

        var request = new CreateProjectRequest
        {
            Name = Name,
            Description = Description,
        };

        var result = await _service.StoreProject(request);
        if (result.Data is null)
        {
            ErrorMessage = result.Message;
            return;
        }

        await Shell.Current.GoToAsync("..");
    }
}