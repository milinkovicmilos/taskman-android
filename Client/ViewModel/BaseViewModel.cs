using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.ViewModel;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool _isBusy;

    [ObservableProperty] private string _pageTitle;
    public bool IsNotBusy => !IsBusy;
}