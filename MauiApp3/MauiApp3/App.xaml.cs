using SharedLib;
using Serilog;

namespace MauiApp3;

public partial class App : Application
{
  public App(IServiceProvider serviceProvider)
  {
    InitializeComponent();
    var fileUtils = serviceProvider.GetService<IFileUtils>();
    if (fileUtils is null) return;
    Console.WriteLine("App: Get fileUtils instance reference.");
    var settings_json_root = fileUtils.GetSettingsJsonRoot();
    var log_folder_name = settings_json_root.GetProperty("downloader_log_folder").GetString() ?? string.Empty;
    var log_folder_dir = Path.Combine(
      Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TheCave",
        log_folder_name);
    if (!Directory.Exists(log_folder_dir))
      throw new DirectoryNotFoundException($"{log_folder_dir} is not found.");
    var log_file_name = $"log-{DateTime.Now:yyyy-MM-dd}.txt";
    var log_file_path = Path.Combine(log_folder_dir, log_file_name);
    Log.Logger = new LoggerConfiguration()
      .MinimumLevel.Debug() // set log level
      .WriteTo.File(
        path: log_file_path,
        rollingInterval: RollingInterval.Infinite,
        flushToDiskInterval: TimeSpan.FromMilliseconds(1500),
        outputTemplate: "{Timestamp:HH:mm:ss} [{Level:u3}] {Message}{NewLine}{Exception}",
        shared: true
      /*
      SetLogger @#down.logger*/
      )
      .CreateLogger();
    Console.WriteLine("App: Logger set.");

    MainPage = new AppShell();
    /*
    App.cs @#main*/
  }
}