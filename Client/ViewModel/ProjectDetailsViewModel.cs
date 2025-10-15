using Client.Model.Tasks;
using Client.Services;
using Client.View.Projects;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModel;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ProjectDetailsViewModel : BaseViewModel
{
    [ObservableProperty] private int _id;
    [ObservableProperty] private string _name;
    [ObservableProperty] private string _description;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(PageLabel))]
    private int _page = 1;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(PageLabel))]
    private int _lastPage = 1;

    [ObservableProperty] private ICollection<TaskSummary> _tasks = new List<TaskSummary>();

    public string PageLabel => $"Page {Page} of {LastPage}";

    private readonly ProjectsService _service;
    private readonly TasksService _tasksService;

    public ProjectDetailsViewModel(ProjectsService service, TasksService tasksService)
    {
        _service = service;
        _tasksService = tasksService;
    }

    private async Task LoadProject()
    {
        try
        {
            var result = await _service.FetchProjectDetails(Id);
            Name = result.Name;
            Description = result.Description;
        }
        catch (Exception ex)
        {
        }
    }

    private async Task LoadProjectsTasks()
    {
        try
        {
            Tasks = new List<TaskSummary>();
            var result = await _tasksService.FetchProjectsTasks(Id, Page);
            var tasks = result.Data;
            if (!tasks.Any() && Page > 1)
            {
                Page--;
                result = await _tasksService.FetchProjectsTasks(Id, Page);
                tasks = result.Data;
            }

            foreach (var project in tasks)
                Tasks.Add(project);
            LastPage = result.LastPage;
        }
        catch (Exception ex)
        {
        }
    }

    [RelayCommand]
    private async Task LoadProjectDetails()
    {
        IsBusy = true;
        try
        {
            await LoadProject();
            await LoadProjectsTasks();
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
        await LoadProjectsTasks();
    }

    [RelayCommand]
    private async Task LoadPreviousPage()
    {
        if (Page - 1 < 1)
            return;

        Page--;
        await LoadProjectsTasks();
    }

    [RelayCommand]
    private async Task GoToEditPage()
    {
        await Shell.Current.GoToAsync($"{nameof(EditProjectPage)}?Id={Id}");
    }

    [RelayCommand]
    private async Task DeleteProject()
    {
        IsBusy = true;
        try
        {
            await _service.RemoveProject(Id);
            await Shell.Current.GoToAsync("..");
        }
        finally
        {
            IsBusy = false;
        }
    }
}