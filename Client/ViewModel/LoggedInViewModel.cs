using Client.State;
using Client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModel;

public partial class LoggedInViewModel : BaseViewModel
{
    public string FullName => _appState.FullName;

    private AppState _appState;

    public LoggedInViewModel(AppState appState)
    {
        _appState = appState;
        _appState.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(_appState.FullName))
                OnPropertyChanged(nameof(FullName));
        };
    }

    [RelayCommand]
    private async Task GoToProjectsAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(ProjectsPage)}");
    }
}