using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LoggerTest3;

public interface ITester 
{
  void Test1();
}
public class Tester(ILogger<Tester> logger, IHostApplicationLifetime hostApplicationLifetime) : ITester
{
  public void Test1()
  {
    while (true)
    {
      Console.Write("WhatYourName> ");
      var input = Console.ReadLine();
      if (!string.IsNullOrEmpty(input) && input.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
      {
        Console.WriteLine("exit program");
        break;
      }
      if (!string.IsNullOrEmpty(input))
      {
        // logger.MyNameIs(input);
        logger.MyNameIs(input);
      }
    }
    hostApplicationLifetime.StopApplication();
  }
}

internal static partial class LoggerExtensions
{
  [LoggerMessage(LogLevel.Information, "My name is {name}")]
  public static partial void MyNameIs(this ILogger logger, string name);
}