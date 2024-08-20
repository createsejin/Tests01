using System.IO.Pipes;

namespace controller;

class CommanderMessenger
{
  private readonly NamedPipeServerStream _pipe_server;
  private StreamWriter? _stream_writer;
  private StreamReader? _stream_reader;

  public CommanderMessenger()
  {
    _pipe_server = new("commander_pipe", PipeDirection.InOut);
  }
  public void TryConnect_commander_pipe()
  {
    bool _is_good_reader;
    bool _is_good_writer;
    Console.WriteLine("The command_pipe server waiting for connect...");
    _pipe_server.WaitForConnection();
    if (_pipe_server != null)
    {
      _stream_writer = new(_pipe_server);
      _stream_reader = new(_pipe_server);
    }
    if (_stream_writer == null)
    {
      Console.WriteLine("stream writer is null");
      _is_good_writer = false;
    }
    else _is_good_writer = true;
    if (_stream_reader == null)
    {
      Console.WriteLine("stream_reader is null");
      _is_good_reader = false;
    }
    else _is_good_reader = true;
    if (_is_good_writer && _is_good_reader)
      Console.WriteLine("The commander_pipe's client and server are connected.");
  }

  public void Commander_pipe_loop()
  {
    while (true)
    {
      if (_stream_reader != null && _stream_writer != null)
      {
        var request = _stream_reader.ReadLine();
        string[] req_args = [];
        if (request != null)
        {
          Console.WriteLine($"receive msg: {request}");
          req_args = request.Split(' ');
        }
        string response = "";
        if (request != null && request.StartsWith("down") && req_args.Length >= 2)
        {
          string rj_number = req_args[1];
          response = $"accepted download request for {rj_number}";
          Console.WriteLine(response);
        }
        else if (request != null && request == "exit")
        {
          response = "Controller exit.";
          Console.WriteLine(response);
          _stream_writer.WriteLine(response);
          _stream_writer.Flush();
          break;
        }
        _stream_writer.WriteLine(response);
        _stream_writer.Flush();
      }
      else Console.WriteLine("stream_reader is null.\nTry to reconnect to commander_pipe.");
    }
  }

  public void Test001()
  {
    TryConnect_commander_pipe();
    Commander_pipe_loop();
  }
}