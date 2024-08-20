using System.Diagnostics;
using System.Text;
using CliWrap;
using CliWrap.EventStream;

namespace downloader.tests;

class BzUnzipper01
{
  private static readonly string bz_exe = @"C:\Program Files\Bandizip\bz.exe";
  // private static readonly string pwsh_exe = @"C:\Program Files\PowerShell\7\pwsh.exe";
  private static readonly string bz_extract_args =
    @"x -y -cp:65001 -consolemode:utf8 -target:name -o:""{0}"" ""{1}""";
  // 인자 설명
  // x 압축 해제 명령
  // -y 질문하지 않고 자동으로 yes로 처리
  // -cp:65001 code page 65001, UTF-8로 압축 해제 처리
  // -consolemode:utf8 console 출력을 UTF-8로 출력
  // -target:name 압축 파일 이름으로된 폴더를 최상단 폴더로 만든 후 그 폴더에 압축 해제
  // -o:"target" 압축 해제될 target folder 지정
  // 그 뒤는 풀 압축 파일 지정.
  public static string BzExe
  {
    get { return bz_exe; }
  }
  private static readonly string bz_test_archive_args =
    @"t -y -cp:65001 -consolemode:utf8 -target:name -o:""{0}"" ""{1}""";

  public static void CheckMountAndSetWorkingDir(string work_dir)
  {
    // check working dir exist
    if (!Directory.Exists(work_dir))
    {
      // if work_dir not exist, throw FileNotFoundException
      throw new FileNotFoundException("mount Q disk.");
    }
    // if work_dir exist, set current dir to work_dir
    Environment.CurrentDirectory = work_dir;
  }
  public static async Task ExtractingByCliWrap(string work_dir, string archive_path, string target_dir)
  {
    CheckMountAndSetWorkingDir(work_dir);
    var cmd = Cli.Wrap(bz_exe)
      .WithArguments(string.Format(bz_extract_args, target_dir, archive_path))
      .WithWorkingDirectory(work_dir);
    await foreach (var cmdEvent in cmd.ListenAsync())
    {
      switch (cmdEvent)
      {
        case StartedCommandEvent started:
          Console.WriteLine($"Process started; ID: {started.ProcessId}");
          break;
        case StandardOutputCommandEvent stdOut:
          Console.WriteLine($"Out> {stdOut.Text}");
          break;
        case StandardErrorCommandEvent stdErr:
          Console.WriteLine($"Err> {stdErr.Text}");
          break;
        case ExitedCommandEvent exited:
          Console.WriteLine($"Process exited; Code: {exited.ExitCode}");
          break;
      }
    }
  }
  public static void ExtractingByDiag(string work_dir, string archive_path, string target_dir, bool test = false)
  {
    CheckMountAndSetWorkingDir(work_dir);

    string arguments;
    if (test == true) arguments = bz_test_archive_args;
    else arguments = bz_extract_args;

    // define ProcessStartInfo
    var start_info = new ProcessStartInfo
    {
      FileName = bz_exe, // execute file
      Arguments = string.Format(arguments, target_dir, archive_path),
      UseShellExecute = false,
      RedirectStandardOutput = true,
      StandardOutputEncoding = Encoding.UTF8,
    };

    using var process = new Process { StartInfo = start_info };
    process.OutputDataReceived += (sender, e) =>
    {
      if (e.Data != null)
      {
        Console.WriteLine(e.Data);
      }
    };
    process.Start();
    process.BeginOutputReadLine();
    process.WaitForExit();
  }
}