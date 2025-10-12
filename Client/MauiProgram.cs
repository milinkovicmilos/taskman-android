using System.Reflection;
using Client.View;
using Client.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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

        builder.Services.AddSingleton<HomePageViewModel>();

        builder.Services.AddSingleton<HomePage>();
        return builder.Build();
    }
}