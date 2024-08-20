
using System.Collections.Concurrent;

namespace downloader.tests;

class InputOutputConsole
{
  private static ConcurrentQueue<string> commandQueue = new ConcurrentQueue<string>();
  public static void InputCommandLoop()
  {
    while (true)
    {
      Console.WriteLine("cmd> ");
      var command = Console.ReadLine();
      if (command != null) commandQueue.Enqueue(command);
    }
  }

  public static void OutputConsoleLoop()
  {

  }
}