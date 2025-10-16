using Client.Model;
using Client.Services;
using Client.View.Projects;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModel;

public partial class ProjectsViewModel : BaseViewModel
{
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(PageLabel))]
    private int _page = 1;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(PageLabel))]
    private int _lastPage = 1;

    [ObservableProperty] private ICollection<ProjectSummary> _projects = new List<ProjectSummary>();

    public string PageLabel => $"Page {Page} of {LastPage}";

    private readonly ProjectsService _service;

    public ProjectsViewModel(ProjectsService service)
    {
        _service = service;
        PageTitle = "Projects";
    }

    [RelayCommand]
    private async Task LoadProjects()
    {
        IsBusy = true;
        try
        {
            Projects = new List<ProjectSummary>();
            var result = await _service.FetchProjects(Page);
            var projects = result.Data;
            if (!projects.Any() && Page > 1)
            {
                Page--;
                result = await _service.FetchProjects(Page);
                projects = result.Data;
            }

            foreach (var project in projects)
                Projects.Add(project);
            LastPage = result.LastPage;
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task LoadNextPage()
    {
        if (Page + 1 > LastPage)
            return;

        Page++;
        await LoadProjects();
    }

    [RelayCommand]
    private async Task LoadPreviousPage()
    {
        if (Page - 1 < 1)
            return;

        Page--;
        await LoadProjects();
    }

    [RelayCommand]
    private async Task GoToCreateProject()
    {
        await Shell.Current.GoToAsync(nameof(CreateProjectPage));
    }

    [RelayCommand]
    private async Task GoToDetails(int id)
    {
        await Shell.Current.GoToAsync($"{nameof(ProjectDetailsPage)}?Id={id}");
    }
}