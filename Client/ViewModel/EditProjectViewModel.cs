using Client.Model;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModel;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class EditProjectViewModel : BaseViewModel
{
    [ObservableProperty] private int _id;
    [ObservableProperty] private string _name;
    [ObservableProperty] private string _description;
    [ObservableProperty] private string _errorMessage;

    private readonly ProjectsService _service;

    public EditProjectViewModel(ProjectsService service)
    {
        _service = service;
    }

    partial void OnIdChanged(int value)
    {
        SetupProject();
    }

    private async Task SetupProject()
    {
        IsBusy = true;
        try
        {
            var result = await _service.FetchProjectDetails(Id);
            Name = result.Name;
            Description = result.Description;
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task UpdateProject()
    {
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

        var request = new UpdateProjectRequest();
        request.Name = Name;
        request.Description = Description;

        IsBusy = true;
        try
        {
            await _service.UpdateProject(Id, request);
        }
        finally
        {
            IsBusy = false;
            await Shell.Current.GoToAsync("..");
        }
    }
}