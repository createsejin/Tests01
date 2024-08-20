using Notes.Models;
namespace Notes.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage
{
  private readonly string _noteName = "notes.txt";
  private readonly string _downloadPath = @"C:\Users\creat\Downloads";
  private readonly string _fileName = string.Empty;
  public string ItemId { set => LoadNote(value); }
  public NotePage()
  {
    InitializeComponent();
    string random_file_name = $"{Path.GetRandomFileName()}.{_noteName}";
    /*
    GetRandomFilename @#*/
    _fileName = Path.Combine(_downloadPath, random_file_name);
    LoadNote(_fileName);
  }
  private void LoadNote(string file_name)
  {
    Note note_model = new() { FileName = file_name };
    if (File.Exists(file_name))
    {
      note_model.Date = File.GetCreationTime(file_name);
      note_model.Text = File.ReadAllText(file_name);
    }
    BindingContext = note_model;
  }
  private async void SaveButton_Clicked(object sender, EventArgs e)
  {
    // Save the file.
    if (BindingContext is Note note)
      File.WriteAllText(note.FileName, TextEditor.Text);
    await Shell.Current.GoToAsync("..");
  }
  private async void DeleteButton_Clicked(object sender, EventArgs e)
  {
    if (BindingContext is Note note)
    {
      if (File.Exists(note.FileName)) File.Delete(note.FileName);
    }

    await Shell.Current.GoToAsync("..");
  }
}