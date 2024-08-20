using System.Collections.ObjectModel;

namespace Notes.Models;

internal class AllNotes
{
  private readonly string _downloadPath = @"C:\Users\creat\Downloads";
  public ObservableCollection<Note> Notes { get; set; } = []; 
  public AllNotes() => LoadNotes();
  public void LoadNotes()
  {
    Notes.Clear();
    var notes = Directory.EnumerateFiles(_downloadPath, "*.notes.txt")
      .Select(filename => new Note() {
        FileName = filename,
        Text = File.ReadAllText(filename),
        Date = File.GetCreationTime(filename)
      })
      .OrderBy(note => note.Date);
    foreach(var note in notes)
    {
      Notes.Add(note);
    }
  } 
}
