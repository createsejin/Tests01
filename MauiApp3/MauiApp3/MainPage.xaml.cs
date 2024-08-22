using SharedLib;

namespace MauiApp3;

public partial class MainPage : ContentPage
{
  private int _count;

  public MainPage()
  {
    InitializeComponent();
    // var fileUtils = Application.Current?.MainPage?.Handler?.MauiContext?
    //   .Services.GetService<IFileUtils>();
    // if (fileUtils is null) return;
    // Console.WriteLine("Get fileUtils instance reference");
  }

  private void OnCounterClicked(object sender, EventArgs e)
  {
    _count++;

    if (_count == 1)
    {
      CounterBtn.Text = $"Clicked {_count} time";
    }
    else
    {
      CounterBtn.Text = $"Clicked {_count} times";
    }
    Console.WriteLine($"Cilck CounterBtn count = {_count}");
    /*
    OnCounterClicked @#main*/

    SemanticScreenReader.Announce(CounterBtn.Text);
  }
}