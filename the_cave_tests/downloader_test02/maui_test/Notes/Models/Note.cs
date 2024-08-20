namespace Notes.Models;

internal class Note
{
  public string FileName { get; set; } = string.Empty;
  public string Text { get; set; } = string.Empty;
  public DateTime Date { get; set; }
}
