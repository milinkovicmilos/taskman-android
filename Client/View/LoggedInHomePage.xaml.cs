using Client.ViewModel;

namespace Client.View;

public partial class LoggedInHomePage : ContentPage
{
    public LoggedInHomePage(LoggedInViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}