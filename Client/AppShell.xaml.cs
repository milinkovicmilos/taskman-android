using Client.State;
using Client.View;
using Client.View.Projects;
using AppState = Client.State.AppState;

namespace Client;

public partial class AppShell : Shell
{
    public AppShell(AppState appState)
    {
        InitializeComponent();
        BindingContext = appState;

        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(LoggedInHomePage), typeof(LoggedInHomePage));

        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));

        Routing.RegisterRoute(nameof(ProjectsPage), typeof(ProjectsPage));
        Routing.RegisterRoute(nameof(CreateProjectPage), typeof(CreateProjectPage));
        Routing.RegisterRoute(nameof(ProjectDetailsPage), typeof(ProjectDetailsPage));
    }
}