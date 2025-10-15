using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModel;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ProjectDetailsViewModel : BaseViewModel
{
    [ObservableProperty] private int _id;
    [ObservableProperty] private string _name;
    [ObservableProperty] private string _description;

    private readonly ProjectsService _service;

    public ProjectDetailsViewModel(ProjectsService service)
    {
        _service = service;
    }

    partial void OnIdChanged(int value)
    {
        LoadProjectDetailsCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadProjectDetails()
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
}