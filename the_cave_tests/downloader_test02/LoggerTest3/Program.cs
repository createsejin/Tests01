/*
docker run -p 4317:4317 -p 4318:4318 --rm -v C:\Users\creat\Projects\opentelemetry-dotnet-main\examples\Console\otlp-collector-example\config.yaml:/etc/otelcol/config.yaml otel/opentelemetry-collector:latest
C:\Program Files\OpenTelemetry Collector
*/
// using LoggerTest3;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using Microsoft.Extensions.Logging;
// using OpenTelemetry.Logs;

// Console.WriteLine("Program start");

// HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// builder.Services.AddSingleton<ITester, Tester>();
// builder.Logging.AddOpenTelemetry(logging => {
//   logging.AddOtlpExporter();
// });

// using IHost host = builder.Build();
// var host_task = host.RunAsync();

// var tester = host.Services.GetRequiredService<ITester>();
// tester.Test1();

// await host_task;
using Microsoft.Extensions.Logging; 
using OpenTelemetry.Logs;

partial class Program
{
  static void Main(string[] args)
  {
    using var factory = LoggerFactory.Create(builder =>
    {
      builder.AddOpenTelemetry(logging =>
      {
        logging.AddOtlpExporter();
      });
    });
    factory.
    ILogger logger = factory.CreateLogger("Program");
    logger.LogInformation("Hello World! Logging is {Description}.", "fun");
  }

  [LoggerMessage(Level = LogLevel.Information, Message = "Hello World! Logging is {Description}.")]
  static partial void LogStartupMessage(ILogger logger, string description);
}