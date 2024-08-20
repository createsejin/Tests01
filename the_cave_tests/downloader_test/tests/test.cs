using CliWrap;

namespace downloader.tests;

class Test
{
  public static async Task TestSelector(string[] args)
  {
    if (args[0] == "test007") Test007();
    else if (args[0] == "test008") Test008();
    else if (args[0] == "test009") Test009();
    else if (args[0] == "test010") Test010();
    else if (args[0] == "test011") await Test011();
    //@#sel.t
  }

  private static void Test007()
  {
    //@#t7
    string archive_path = @"RJ335944.zip";
    string work_dir = @"Q:\s\voice\(1)미개봉보이스\test";
    string target_dir = $@"target";
    BzUnzipper01.ExtractingByDiag(work_dir, archive_path, target_dir, false);
  }

  private static void Test008()
  {
    string work_dir = @"Q:\s\voice\(1)미개봉보이스\test";
    if (!Directory.Exists(work_dir)) throw new FileNotFoundException("mount Q disk.");
    Environment.CurrentDirectory = work_dir;
    string archive_path = @"RJ01182655.part1.exe";
    string target_dir = @"target";
    var Arguments = string.Format(BzUnzipper01.BzExe, target_dir, archive_path);
    Console.WriteLine($"{Arguments}");
  }

  private static void Test009()
  {
    string work_dir = @"Q:\s\voice\(1)미개봉보이스\test";

    Environment.CurrentDirectory = work_dir;
  }

  private static void Test010()
  {
    string work_dir = @"Q:\s\voice\(1)미개봉보이스\test";
    string archive_path = @"RJ01182655.part1.exe";
    string target = @"target";
    // BzUnzipper.TestArchive(work_dir, archive_path, target);
    BzUnzipper01.ExtractingByDiag(work_dir, archive_path, target);
  }

  private static async Task Test011()
  {
    string work_dir = @"Q:\s\voice\(1)미개봉보이스\test";
    string target_dir = @"target";
    string archive = @"RJ335944.zip";
    BzUnzipper01.CheckMountAndSetWorkingDir(work_dir);
    await BzUnzipper01.ExtractingByCliWrap(work_dir, archive, target_dir);
  }

  //@#t12
  private static void Test012()
  {

  }
}