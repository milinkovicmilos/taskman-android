using Client.Services;
using Client.State;
using Client.View;

namespace Client;

public partial class App : Application
{
    private AppState _appState;
    private AuthService _authService;

    public App(AppState state, AuthService authService)
    {
        InitializeComponent();

        MainPage = new AppShell(state);

        _appState = state;
        _authService = authService;

        Task.Run(async () => await CheckLoginStatus());
    }

    private async Task CheckLoginStatus()
    {
        var token = await SecureStorage.GetAsync("token");
        if (string.IsNullOrEmpty(token))
        {
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
        else
        {
            var result = await _authService.FetchUserAsync();
            if (result != null)
            {
                _appState.IsLoggedIn = true;
                _appState.UserFirstName = result.Data.FirstName;
                _appState.UserLastName = result.Data.LastName;
                _appState.UserEmail = result.Data.Email;
            }

            await Shell.Current.GoToAsync($"//{nameof(LoggedInHomePage)}");
        }
    }
}