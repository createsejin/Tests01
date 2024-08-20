using CliWrap;

namespace WavConvertor;

class FFmpeg
{
  private Command cmd;
  private readonly string ffmpeg_exe = @"C:\ffmpeg\bin\ffmpeg.exe";
  // ffmpeg -i 
  private string wav_to_mp3_args = @"";

  public void Test001()
  {
  }
}