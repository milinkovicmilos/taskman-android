using Client.Enums;
using Client.Model.Tasks;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModel.Tasks;

[QueryProperty(nameof(ProjectId), nameof(ProjectId))]
public partial class CreateTaskViewModel : BaseViewModel
{
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _description;
    [ObservableProperty] private PriorityLevelOptions _priority = Enums.PriorityLevelOptions.Unset;
    [ObservableProperty] private DateTime? _dueDate;
    [ObservableProperty] private string _errorMessage;
    [ObservableProperty] private int _projectId;

    public ICollection<PriorityLevelOptions> PriorityLevelOptions =>
        Enum.GetValues(typeof(PriorityLevelOptions)).Cast<PriorityLevelOptions>().ToList();

    public DateTime MinimumDate => DateTime.Today;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(DueDateButtonText))]
    private bool _dueDateEnabled;

    public string DueDateButtonText => DueDateEnabled ? "Clear Date" : "Add Date";

    private readonly TasksService _tasksService;

    public CreateTaskViewModel(TasksService tasksService)
    {
        _tasksService = tasksService;
        PageTitle = "Create Task";
    }

    [RelayCommand]
    private async Task CreateTaskAsync()
    {
        ErrorMessage = string.Empty;

        if (string.IsNullOrEmpty(Title))
        {
            ErrorMessage = "Task title cannot be empty.";
            return;
        }

        if (string.IsNullOrEmpty(Description))
        {
            ErrorMessage = "Task description cannot be empty.";
            return;
        }

        var request = new CreateTaskRequest
        {
            Title = Title,
            Description = Description,
            DueDate = DueDateEnabled ? DueDate : null,
            Priority = Priority != Enums.PriorityLevelOptions.Unset ? Priority : null,
        };

        IsBusy = true;
        try
        {
            var result = await _tasksService.StoreTaskAsync(ProjectId, request);
            if (result.Data is not null)
            {
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                ErrorMessage = result.Message;
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task ClearDateAsync()
    {
        DueDateEnabled = !DueDateEnabled;
    }
}