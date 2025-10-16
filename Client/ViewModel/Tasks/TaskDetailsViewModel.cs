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
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(TaskCompletionButtonText))]
    private TaskDetails? _task;

    public int ProjectId { get; set; }
    public int TaskId { get; set; }

    public string TaskCompletionButtonText
    {
        get
        {
            if (Task is null)
                return string.Empty;

            return Task.Completed ? "Mark as Incomplete" : "Mark as Complete";
        }
    }

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
            var result = await _tasksService.FetchTaskDetailsAsync(ProjectId, TaskId);

            Task = result;
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task TaskCompletionButtonAsync()
    {
        if (Task is null)
            return;

        IsBusy = true;
        try
        {
            if (Task.Completed)
                await _tasksService.MarkTaskAsIncompleteAsync(ProjectId, TaskId);
            else
                await _tasksService.MarkTaskAsCompleteAsync(ProjectId, TaskId);
        }
        finally
        {
            await LoadTaskDetailsAsync();
            IsBusy = false;
        }
    }
}