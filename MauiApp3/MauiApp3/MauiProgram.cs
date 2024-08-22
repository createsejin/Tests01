using Microsoft.Extensions.Logging;
using SharedLib;

namespace MauiApp3;

public static class MauiProgram
{
  public static MauiApp CreateMauiApp()
  {
    var builder = MauiApp.CreateBuilder();
    builder
      .UseMauiApp<App>()
      .ConfigureFonts(fonts =>
      {
        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
      });
    builder.Services.AddSingleton<IFileUtils, FileUtils>();
    /*
    Program @#main*/

#if DEBUG
    builder.Logging.AddDebug();
    builder.Logging.SetMinimumLevel(LogLevel.Debug);
#endif
    return builder.Build();
  }
}