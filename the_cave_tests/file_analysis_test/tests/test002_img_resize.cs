using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;

namespace file_analysis_test.tests;

class ImgProcess
{
  public static void ResizeImg(
    string img_path, string suffix, int frame_width, int frame_height, int quality)
  {
    // quality value valid check
    if (quality < 1 || quality > 100)
    {
      throw new ArgumentOutOfRangeException(nameof(quality),
        "The value must be not less than 1 and not more than 100.");
    }

    // save_path는 suffix가 붙는 형태가 된다.
    string img_name = Path.GetFileNameWithoutExtension(img_path);
    string? img_dir = Path.GetDirectoryName(img_path);
    string save_path = $@"{img_dir}\{img_name}{suffix}.jpg";

#pragma warning disable CA1416 // Validate platform compatibility
    Image original_img = Image.FromFile(img_path);
    int original_width = original_img.Width;
    int original_height = original_img.Height;
    // original의 가로 세로 size가 모두 frame의 가로 세로 size보다 작거나 같으면 이미지를 리사이즈 하지 않고
    // suffix가 붙은 이름으로 기존 파일을 copy 한다.
    if (original_width <= frame_width && original_height <= frame_height)
    {
      File.Copy(img_path, save_path, true);
      Console.WriteLine($"The img size is smaller or same to frame size: ({frame_width}:{frame_height})");
      Console.WriteLine($"Copied img saved to:\n{save_path}");
      return;
    }

    double ratio = (double)original_width / original_height;

    int new_width;
    int new_height;
    if (original_width > original_height)
    {
      // frame_width 기준으로 new_height를 구하는 공식
      new_height = (int)Math.Round(frame_width / ratio);
      new_width = frame_width;
      // 만약 위에서 크기를 조절했는데도 new_height가 frame_height를 초과하면,
      if (new_height > frame_height)
      {
        // 다시 frame_height 기준으로 new_width를 구한다.
        new_height = frame_height;
        // frame_height 기준으로 new_width를 구하는 공식
        new_width = (int)Math.Round(frame_height * ratio);
      }
    }
    else
    {
      // frame_height 기준으로 new_width를 구하는 공식
      new_width = (int)Math.Round(frame_height * ratio);
      new_height = frame_height;
      // new_width가 frame_width를 초과했을때
      if (new_width > frame_width)
      {
        // 다시 frame_width 기준으로 new_height를 구한다.
        // 이 부분은 frame_width가 frame_height보다 작을때에만 적용된다.
        new_width = frame_width;
        new_height = (int)Math.Round(frame_width / ratio);
      }
    }
    var resized_img = new Bitmap(new_width, new_height, PixelFormat.Format24bppRgb);
    using var graphics = Graphics.FromImage(resized_img);
    {
      // graphics quality options
      graphics.CompositingQuality = CompositingQuality.HighQuality;
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.SmoothingMode = SmoothingMode.HighQuality;
      graphics.DrawImage(original_img, 0, 0, new_width, new_height);
    }
    // Get an ImageCodecInfo object that represents the JPEG codec.
    ImageCodecInfo imageCodecInfo = GetEncoderInfo(ImageFormat.Jpeg);
    // Create an Encoder object for the Quality parameter.
    Encoder encoder = Encoder.Quality;
    // Create an EncoderParameters object. 
    var encoderParameters = new EncoderParameters(1);
    // Save the image as a JPEG file with quality level.
    var encoderParameter = new EncoderParameter(encoder, quality);
    encoderParameters.Param[0] = encoderParameter;
    resized_img.Save(save_path);
    Console.WriteLine($"resized img saved to:\n{save_path}");
  }

  /// <summary>
  /// Method to get encoder infor for given image format.
  /// </summary>
  /// <param name="format">Image format</param>
  /// <returns>image codec info.</returns>
  private static ImageCodecInfo GetEncoderInfo(ImageFormat format) =>
    ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid) ??
     throw new ArgumentNullException(nameof(format));
#pragma warning restore CA1416 // Validate platform compatibility

  private static int[] GetSizeFromSampleSize(int sample_w, int sample_h, int dest_w)
  {
    double ratio = (double)(sample_w / sample_h);
    int dest_h = (int)Math.Round(dest_w / ratio);
    return [dest_w, dest_h];
  }

  public static void Test003()
  {
    string sample_path = @"S:\s\samples\RJ272335_천사의 치유\天使の癒しのマゾ虐め\イラスト\パッケージ.png";
    int sample_w = 300;
    int sample_h = 216;
    var small_size = GetSizeFromSampleSize(sample_w, sample_h, 170);
    var middle_size = GetSizeFromSampleSize(sample_w, sample_h, 320);
    var big_size = GetSizeFromSampleSize(sample_w, sample_h, 600);
    ResizeImg(sample_path, "_small", small_size[0], small_size[1], 87);
    ResizeImg(sample_path, "_middle", middle_size[0], middle_size[1], 87);
    ResizeImg(sample_path, "_big", big_size[0], big_size[1], 87);
  }

  public static void Test004()
  {
    string sample_dir = @"S:\s\samples\RJ272335_천사의 치유\天使の癒しのマゾ虐め\イラスト\パッケージ.png";
    int number = 1;
    string output_str = string.Format(@"{0}\{1}_output{2:000}.png", Path.GetDirectoryName(sample_dir),
      Path.GetFileNameWithoutExtension(sample_dir), number);
    Console.WriteLine($"{output_str}");
  }

  public static void Test005()
  {
    string smample_path = @"S:\s\samples\RJ272335_천사의 치유\天使の癒しのマゾ虐め\イラスト\パッケージ_small.jpg";
    ResizeImg(smample_path, "_test", 300, 216, 67);
  }
}


