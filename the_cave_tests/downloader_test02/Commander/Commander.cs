using System.IO.Pipes;

namespace commander;

class Commander
{
  private string[] _args;
  private bool _is_exit;
  private readonly NamedPipeClientStream _pipe_client;
  private StreamWriter? _stream_writer;
  private StreamReader? _stream_reader;

  public Commander()
  {
    _args = [];
    _is_exit = false;
    _pipe_client = new(".", "commander_pipe", PipeDirection.InOut);
  }
  public void TryConnect()
  {
    bool _is_good_writer;
    bool _is_good_reader;
    Console.WriteLine("Attempting to connect to Controller server...");
    _pipe_client.Connect();
    if (_pipe_client != null)
    {
      _stream_writer = new(_pipe_client);
      _stream_reader = new(_pipe_client);
    }
    if (_stream_writer == null)
    {
      Console.WriteLine("stream writer is null");
      _is_good_writer = false;
    }
    else _is_good_writer = true;
    if (_stream_reader == null)
    {
      Console.WriteLine("steam reader is null");
      _is_good_reader = false;
    }
    else _is_good_reader = true;
    if (_is_good_writer && _is_good_reader)
      Console.WriteLine("Connected to Controller server.");
  }

  private void DownloadRequest()
  {
    string rj_number;
    if (_args.Length == 1)
    {
      Console.WriteLine("down command need <rj_number> argument.");
      Console.WriteLine("Useage:");
      Console.WriteLine("cmd> down <rj_number>\n");
      return;
    }
    else if (_args.Length > 2)
    {
      Console.WriteLine("Too many arguments");
      return;
    }
    else rj_number = _args[1];

    if (!string.IsNullOrEmpty(rj_number))
    {
      Console.WriteLine($"request download {rj_number}...");
      if (_stream_writer != null && _stream_reader != null)
      {
        _stream_writer.WriteLine($"down {rj_number}");
        _stream_writer.Flush();

        var response = _stream_reader.ReadLine();
        Console.WriteLine($"response: {response}");
      }
      else
      {
        Console.WriteLine("stream writer is null.");
        Console.WriteLine("Try reconnect to Controller.");
      }
    }
    else Console.WriteLine("argument not enough. give collection number.");
  }

  private void ExitPrograms()
  {
    if (_stream_reader != null && _stream_writer != null)
    {
      _stream_writer.WriteLine("exit");
      _stream_writer.Flush();

      var response = _stream_reader.ReadLine();
      Console.WriteLine($"exit response: {response}");
    }
    _is_exit = true;
    Console.WriteLine("program exit.");
  }

  private void CommandParser(string input)
  {
    _args = input.Split(' ');
    string main_cmd = _args[0];
    if (main_cmd == "exit" || main_cmd == "quit" || main_cmd == "q")
      ExitPrograms();
    else if (main_cmd == "down") DownloadRequest();
    else
      Console.WriteLine($"|{input}| is unknown arguments");
  }

  public void CommandLoop()
  {
    while (true)
    {
      Console.Write("cmd> ");
      var input = Console.ReadLine();
      if (string.IsNullOrEmpty(input))
      {
        Console.WriteLine("input is empty.");
      }
      else if (input != null)
      {
        CommandParser(input);
        if (_is_exit) break;
      }
      else
        Console.WriteLine("input is null");
    }
  }
  public void Test001()
  {
    TryConnect();
    CommandLoop();
  }
}