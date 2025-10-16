using System.Text.RegularExpressions;
using Client.Model;
using Client.Services;
using Client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModel;

public partial class RegisterViewModel : BaseViewModel
{
    [ObservableProperty] private string firstName;
    [ObservableProperty] private string lastName;
    [ObservableProperty] private string email;
    [ObservableProperty] private string password;
    [ObservableProperty] private string passwordConfirmation;
    [ObservableProperty] private string errorMessage;

    private AuthService _authService;

    public RegisterViewModel(AuthService authService)
    {
        _authService = authService;
        PageTitle = "Register";
    }

    [RelayCommand]
    private async Task RegisterAsync()
    {
        // Clear previous error
        ErrorMessage = string.Empty;

        if (string.IsNullOrEmpty(FirstName))
        {
            ErrorMessage = "Please enter first name.";
            return;
        }

        if (string.IsNullOrEmpty(LastName))
        {
            ErrorMessage = "Please enter last name.";
            return;
        }

        if (string.IsNullOrEmpty(Email))
        {
            ErrorMessage = "Please enter email.";
            return;
        }

        if (string.IsNullOrEmpty(Password))
        {
            ErrorMessage = "Please enter password.";
            return;
        }
        else
        {
            if (!Password.Any(x => char.IsPunctuation(x) || char.IsSymbol(x)))
            {
                ErrorMessage = "Password needs to contain at least one symbol character.";
                return;
            }

            var UpperCaseRegex = new Regex(@"[A-Z]+");
            if (!UpperCaseRegex.IsMatch(Password))
            {
                ErrorMessage = "Password must contain at least one upper case letter.";
                return;
            }

            var LowerCaseRegex = new Regex(@"[a-z]+");
            if (!LowerCaseRegex.IsMatch(Password))
            {
                ErrorMessage = "Password must contain at least one lower case letter.";
                return;
            }

            if (!Password.Any(x => char.IsDigit(x)))
            {
                ErrorMessage = "Password must contain at least one digit.";
                return;
            }

            if (Password.Length < 8)
            {
                ErrorMessage = "Password must be at least 8 characters long.";
                return;
            }

            if (PasswordConfirmation != Password)
            {
                ErrorMessage = "Passwords do not match.";
                return;
            }
        }

        var request = new RegisterRequest
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Password = Password,
            PasswordConfirmation = PasswordConfirmation,
        };

        var result = await _authService.RegisterAsync(request);
        if (result.Data is not null)
        {
            await Shell.Current.GoToAsync($"//{nameof(LoggedInHomePage)}");
        }
        else
        {
            ErrorMessage = result.Message;
        }
    }
}