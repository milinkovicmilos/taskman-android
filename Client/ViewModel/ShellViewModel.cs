using Client.State;

namespace Client.ViewModel;

public class ShellViewModel : BaseViewModel
{
    public AppState AppState { get; }
    public AppStateCommands AppStateCommands { get; }

    public ShellViewModel(AppState appState, AppStateCommands appStateCommands)
    {
        AppState = appState;
        AppStateCommands = appStateCommands;
    }
}