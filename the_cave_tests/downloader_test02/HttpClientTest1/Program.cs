// See https://aka.ms/new-console-template for more information
using System.Reflection;

Console.WriteLine("Test start");
if (int.TryParse(args[0], out int class_num) && int.TryParse(args[1], out int method_num))
{
  string test_class_name = $"HttpClientTest1.Tests{class_num}";
  string test_method_name = $"Test{method_num}";
  Type? classType = Type.GetType(test_class_name);
  MethodInfo? methodInfo = classType?.GetMethod(test_method_name);
  if (classType is not null)
  {
    object? test_class = Activator.CreateInstance(classType);
    methodInfo?.Invoke(test_class, null);
  }
}
