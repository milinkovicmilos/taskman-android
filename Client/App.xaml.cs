using Client.Services;
using Client.View;
using Client.ViewModel;

namespace Client;

public partial class App : Application
{
    private readonly AuthService _authService;
    private ShellViewModel _viewModel;

    public App(ShellViewModel viewModel, AuthService authService)
    {
        InitializeComponent();
        _viewModel = viewModel;
        _authService = authService;

        MainPage = new AppShell(viewModel);
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
                _viewModel.AppState.IsLoggedIn = true;
                _viewModel.AppState.UserFirstName = result.Data.FirstName;
                _viewModel.AppState.UserLastName = result.Data.LastName;
                _viewModel.AppState.UserEmail = result.Data.Email;
            }

            await Shell.Current.GoToAsync($"//{nameof(LoggedInHomePage)}");
        }
    }
}