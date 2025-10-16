using Client.Enums;
using Client.Model.Tasks;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModel.Tasks;

[QueryProperty(nameof(ProjectId), nameof(ProjectId))]
[QueryProperty(nameof(TaskId), nameof(TaskId))]
public partial class TaskDetailsViewModel : BaseViewModel
{
    [ObservableProperty] private TaskDetails _task;

    public int ProjectId { get; set; }
    public int TaskId { get; set; }

    private readonly TasksService _tasksService;

    public TaskDetailsViewModel(TasksService tasksService)
    {
        _tasksService = tasksService;
        PageTitle = "Task Details";
    }

    [RelayCommand]
    private async Task LoadTaskDetailsAsync()
    {
        IsBusy = true;
        try
        {
            var result = await _tasksService.FetchTaskDetails(ProjectId, TaskId);

            Task = result;
        }
        finally
        {
            IsBusy = false;
        }
    }
}