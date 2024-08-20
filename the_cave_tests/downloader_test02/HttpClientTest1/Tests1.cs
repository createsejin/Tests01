using System.Text.Json;

namespace HttpClientTest1;

public class Tests1
{
  // private readonly HttpClient _client;
  // private readonly string _rapidLoginUrl = "https://rapidgator.net/auth/login";
  private readonly string _testJson = @"Q:\s\ddd.json";
  public Tests1()
  {
    // _client = new()
    // {
    //   BaseAddress = new Uri(_rapidLoginUrl)
    // };
  }
  public void Test1()
  {
    if (File.Exists(_testJson))
    {
      string json_str = File.ReadAllText(_testJson);
      using JsonDocument document = JsonDocument.Parse(json_str);
      JsonElement root = document.RootElement;
      string? email = root.GetProperty("M").GetString();
      string? passwd = root.GetProperty("D").GetString();
      Console.WriteLine($"email: {email}");
      Console.WriteLine($"passwd: {passwd}");
    }
    else Console.WriteLine($"{_testJson} is not exist.");
  }
}
