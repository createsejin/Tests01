namespace file_analysis_test;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("test program start\n");
    // console 출력을 UTF-8로 지정한다.
    Console.OutputEncoding = System.Text.Encoding.UTF8;

    // print top level collection directory files and dirs
    if (args[0] == "test001") tests.FileAnalyzer.Test001();
    // find img files
    else if (args[0] == "test002") tests.FileAnalyzer.Test002();
    // make three size thumbnail img
    else if (args[0] == "test003") tests.ImgProcess.Test003();
    else if (args[0] == "test004") tests.ImgProcess.Test004();
    else if (args[0] == "test005") tests.ImgProcess.Test005();
    // img_dir_keywords search and img_extension search
    else if (args[0] == "test006") tests.FileAnalyzer.Test006();
  }
}
