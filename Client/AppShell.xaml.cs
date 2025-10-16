using Client.State;
using Client.View;
using Client.View.Projects;
using Client.ViewModel;
using AppState = Client.State.AppState;

namespace Client;

public partial class AppShell : Shell
{
    public AppShell(ShellViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(LoggedInHomePage), typeof(LoggedInHomePage));

        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));

        Routing.RegisterRoute(nameof(ProjectsPage), typeof(ProjectsPage));
        Routing.RegisterRoute(nameof(CreateProjectPage), typeof(CreateProjectPage));
        Routing.RegisterRoute(nameof(ProjectDetailsPage), typeof(ProjectDetailsPage));
        Routing.RegisterRoute(nameof(EditProjectPage), typeof(EditProjectPage));
    }
}