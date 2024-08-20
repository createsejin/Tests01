namespace file_analysis_test.tests;

class FileAnalyzer
{
  public static void PrintCurrentDir(string target)
  {
    // 일반 file들을 출력한다.
    Console.WriteLine("Files:");
    // target으로부터 일반 file들의 목록 리스트를 받는다.
    var files = Directory.GetFiles(target);
    // 만약 일반 file이 하나도 없다면,
    if (files.GetLength(0) == 0)
    {
      // 메세지를 출력한다.
      Console.WriteLine("Files not exist in current tartget directory.");
    }
    else // 그게 아니라면
    {
      foreach (var file in files)
      {
        // file들의 filename들을 출력해준다.
        Console.WriteLine(Path.GetFileName(file));
      }
    }

    // directory들을 출력한다. 
    Console.WriteLine("\nDirectories:");
    // 얻은 directory들 중, 제외할 dir들
    string[] exclude_dirs = [@"(1)미개봉보이스", @"(3)미평가"];
    // 제외할 dir들의 pull path dir이 담길 List 선언
    var exclude_paths = new List<string>();
    foreach (var dir in exclude_dirs)
    {
      // target prefix를 붙여서 pull path 형식으로 만든다.
      string path = $@"{target}\{dir}";
      exclude_paths.Add(path);
    }
    // 현재 타겟의 모든 directory들을 얻고, exclude_paths들은 제외시킨다.
    string[] directories = Directory.GetDirectories(target).Except(exclude_paths).ToArray();
    foreach (var directory in directories)
    {
      // 제외 목록이 제거된 path들의 filename을 콘솔에 출력해준다.
      Console.WriteLine($"{Path.GetFileName(directory)}");
    }
  }

  private static bool IsMountDisk(string target, string disk)
  {
    // 만약 target이 존재하지 않는다면,
    if (!Directory.Exists(target))
    {
      // 필요한 disk를 마운트하라고 알려주고,
      Console.WriteLine($"{target} not exist. Mount {disk} disk.");
      return false; // 메소드를 종료시킨다.
    }
    return true;
  }

  private static void EnumerateImgFiles(string path)
  {
    // 이미지 확장자 배열
    string[] img_extensions = ["*.jpg", "*.jpeg", "*.png"];
    string[] exclude = ["hentai", "www"];
    var img_files = img_extensions
      // img_extensions 배열에 담긴 ext가 포함된 모든 file들의 string들을 img_files에 담는다.
      .SelectMany(ext => Directory.EnumerateFiles(path, ext, SearchOption.AllDirectories))
      // 결과를 array로 만든 후, file 중에서 그 이름에 exclude 배열에 담긴 keyword 중 
      // 어느 하나라도 포함된다면 그것은 제외한다.
      .ToArray().Where(file => !exclude.Any(keyword => Path.GetFileName(file).Contains(keyword)));
    // 만약 img_files가 하나라도 있다면,
    if (img_files.Any())
    {
      // 그 이미지 파일들을 콘솔에 출력한다.
      foreach (var file in img_files)
      {
        Console.WriteLine($"{Path.GetFileName(file)}");
      }
      Console.WriteLine();
    }
    // 하나도 없으면 없다고 메세지를 출력한다.
    else
    {
      Console.WriteLine("There is no result of img_files file name search.");
      Console.WriteLine();
    }
  }

  public static void Test001()
  {
    // 파일 목록을 읽을 target의 path
    string target = @"S:\s\voice";
    if (!IsMountDisk(target, "S")) return;
    // 그게 아니라면 현재 working directory를 target으로 바꾼다.
    Environment.CurrentDirectory = target;

    PrintCurrentDir(target);
  }

  public static void Test002()
  {
    // sample file들이 있는 target path이다.
    string target = @"S:\s\samples";
    if (!IsMountDisk(target, "S")) return;
    // 그게 아니라면 현재 working directory를 target으로 바꾼다.
    Environment.CurrentDirectory = target;

    // sample의 root_dirs 배열을 얻는다.
    string[] root_dirs = Directory.GetDirectories(target);
    foreach (var dir in root_dirs)
    {
      // root_dir print
      Console.WriteLine($"{Path.GetFileName(dir)}:");
      // 특정 확장자의 이미지 파일만 골라서 각각 그 결과들을 concat한다.
      var img_files = Directory.EnumerateFiles(dir, "*.jpg", SearchOption.AllDirectories)
        .Concat(Directory.EnumerateFiles(dir, "*.jpeg", SearchOption.AllDirectories))
        .Concat(Directory.EnumerateFiles(dir, "*.png", SearchOption.AllDirectories))
        // img_file name에 특정 단어가 있으면 그것은 제외시킨다.
        .Where(file => !Path.GetFileName(file).Contains("hentai"));
      if (!img_files.Any()) Console.WriteLine("The title img file not exist.");
      else
      {
        foreach (var file in img_files)
        {
          Console.WriteLine($"{Path.GetFileName(file)}");
        }
      }
      Console.WriteLine();
    }
  }

  public static void Test006()
  {
    // sample file들이 있는 target path이다.
    string target = @"S:\s\samples";
    if (!IsMountDisk(target, "S")) return;
    // 그게 아니라면 현재 working directory를 target으로 바꾼다.
    Environment.CurrentDirectory = target;

    // sample의 root_dirs 배열을 얻는다.
    string[] root_dirs = Directory.GetDirectories(target);

    // 이미지가 들어있을 것으로 예상되는 폴더들의 keyword들이다.
    string[] img_dir_keywords = ["イラスト", "画像"];
    // root의 각 directory들을 루핑한다.
    foreach (var dir in root_dirs)
    {
      // 각 dir들의 name을 출력한다.
      Console.WriteLine($"{Path.GetFileName(dir)}:");
      // 각 dir들의 하위 sub_dir들을 모두 검색해본다.
      var sub_dirs = Directory.EnumerateDirectories(dir, "*", SearchOption.AllDirectories)
      // 각 sub_dir 중에서 img_dir_keywords 중 어느 하나라도 그 sub_dir의 directory name이 그 keyword를 포함한다면,
      // 그것들을 select 한다.
        .Where(sub_dir => img_dir_keywords.Any(keyword => Path.GetFileName(sub_dir).Contains(keyword)));
      // 만약 하나 이상이라도 그 결과가 있다면,
      if (sub_dirs.Any())
      {
        foreach (var sub_dir in sub_dirs)
        {
          // 일단 그 결과를 출력해주고,
          Console.WriteLine($"{sub_dir}");
          // 그 sub_dir 내부 파일들을 Enumeration해서 이미지 확장자를 가진 파일들을 찾는다.
          EnumerateImgFiles(sub_dir);
        }
      }
      else // 만약 img_dir_keywords search 결과물이 없다면,
      {
        Console.WriteLine("There is no result of img_dir_keywords dir name search.");
        // root_dirs 내부의 dir 내부의 모든 file들을 Enumerate 해서 이미지 파일들을 찾는다.
        EnumerateImgFiles(dir);
      }
    }
  }
}

