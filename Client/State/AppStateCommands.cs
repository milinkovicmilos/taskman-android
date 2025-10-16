using System.Windows.Input;
using Client.Services;
using Client.View;
using CommunityToolkit.Mvvm.Input;

namespace Client.State;

public partial class AppStateCommands
{
    private readonly AuthService _authService;
    private AppState _appState;

    public AppStateCommands(AuthService authService, AppState appState)
    {
        _authService = authService;
        _appState = appState;
    }

    [RelayCommand]
    private async Task LogoutAsync()
    {
        try
        {
            await _authService.LogoutAsync();

            SecureStorage.Remove("token");
            _appState.IsLoggedIn = false;

            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
        catch (Exception e)
        {
        }
    }
}