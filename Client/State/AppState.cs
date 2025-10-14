using Client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.State;

public partial class AppState : ObservableObject
{
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(IsNotLoggedIn))]
    private bool isLoggedIn;

    public bool IsNotLoggedIn => !IsLoggedIn;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(FullName))]
    private string userFirstName = string.Empty;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(FullName))]
    private string userLastName = string.Empty;

    public string FullName => $"{UserFirstName} {UserLastName}";

    [ObservableProperty] private string userEmail = string.Empty;

    [RelayCommand]
    private async Task LogoutAsync()
    {
        SecureStorage.Remove("token");
        IsLoggedIn = false;

        await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
    }
}