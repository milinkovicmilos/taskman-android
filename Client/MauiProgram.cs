using System.Reflection;
using Client.Services;
using Client.State;
using Client.View;
using Client.View.Projects;
using Client.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AppState = Client.State.AppState;

namespace Client;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
#if DEBUG
        builder.Logging.AddDebug();
        const string ConfigFileName = "Client.Config.appsettings.Development.json";
#else
        const string ConfigFileName = "Client.Config.appsettings.Production.json";
#endif
        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream(ConfigFileName);
        if (stream != null)
        {
            var configuration = new ConfigurationBuilder().AddJsonStream(stream).Build();
            builder.Configuration.AddConfiguration(configuration);
        }

        builder.Services.AddSingleton<HttpClientService>();
        builder.Services.AddTransient<AuthService>();
        builder.Services.AddSingleton<ProjectsService>();

        builder.Services.AddSingleton<HomePageViewModel>();
        builder.Services.AddSingleton<LoggedInViewModel>();
        builder.Services.AddTransient<RegisterViewModel>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<ProjectsViewModel>();
        builder.Services.AddTransient<CreateProjectViewModel>();
        builder.Services.AddTransient<ProjectDetailsViewModel>();

        builder.Services.AddSingleton<AppState>();

        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddTransient<LoggedInHomePage>();
        builder.Services.AddTransient<RegisterPage>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<ProjectsPage>();
        builder.Services.AddTransient<CreateProjectPage>();
        builder.Services.AddTransient<ProjectDetailsPage>();

        return builder.Build();
    }
}