using Client.View;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModel;

public partial class HomePageViewModel : BaseViewModel
{
    [RelayCommand]
    private async Task GoToLoginAsync()
    {
        Shell.Current.GoToAsync(nameof(LoginPage));
    }

    [RelayCommand]
    private async Task GoToRegisterAsync()
    {
        Shell.Current.GoToAsync(nameof(RegisterPage));
    }
}