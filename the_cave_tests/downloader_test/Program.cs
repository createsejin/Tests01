namespace downloader;

using downloader.tests;
using Grpc.Core;
using Messenger; // 생성된 C# 네임스페이스
using System.Threading.Tasks;

class MessengerServiceImpl : Messenger.MessengerBase
{
  public override Task<MessageResponse> SendMessage(Message request, ServerCallContext context)
  {
    Console.WriteLine("Received message: " + request.Text);

    // 메시지 처리 (간단한 예시)
    var response = new MessageResponse { Response = "Message processed: " + request.Text };
    return Task.FromResult(response);
  }
}

class Program
{
  private static readonly int Port = 50051;

  public static async Task Main(string[] args)
  {
    Console.WriteLine("program start");
    Console.OutputEncoding = System.Text.Encoding.UTF8;
    await Test.TestSelector(args);
  }
  public static async Task Main001(string[] args)
  {
    Console.WriteLine("program start");
    var server = new Server
    {
      Services = { Messenger.BindService(new MessengerServiceImpl()) },
      Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
    };
    server.Start();

    Console.WriteLine("Messenger server listening on port " + Port);

    Console.WriteLine("Press any key to stop the server...");
    Console.ReadKey();

    await server.ShutdownAsync();
  }
}
