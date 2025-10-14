using Client.Model;
using Client.Services;
using Client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModel;

public partial class LoginViewModel : BaseViewModel
{
    [ObservableProperty] private string email;

    [ObservableProperty] private string password;

    [ObservableProperty] private string errorMessage;

    private AuthService _authService;

    public LoginViewModel(AuthService authService)
    {
        _authService = authService;
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        // Clear previous error
        ErrorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Please enter both email and password.";
            return;
        }

        var request = new LoginRequest
        {
            Email = email,
            Password = password,
        };

        var result = await _authService.LoginAsync(request);
        if (result.Data != null)
        {
            await Shell.Current.GoToAsync($"//{nameof(LoggedInHomePage)}");
        }
        else
        {
            ErrorMessage = result.Message;
        }
    }
}