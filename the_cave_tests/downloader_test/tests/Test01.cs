using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace test01;

public class Test01
{
  public static void Test001()
  {
    Console.WriteLine("test001 called.");
  }

  public static void Test002()
  {
    // start session
    var options = new FirefoxOptions();
    options.AddArgument("-headless");
    var driver = new FirefoxDriver(options);
    // goto url
    driver.Navigate().GoToUrl("https://www.selenium.dev/selenium/web/web-form.html");
    // request browser information: title
    var title = driver.Title;
    Console.WriteLine($"title: {title}");
    // establish waiting strategy
    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
    // find elements
    var textBox = driver.FindElement(By.Name("my-text"));
    var submitButton = driver.FindElement(By.TagName("button"));
    // take actions on elements
    textBox.SendKeys("Selenium");
    submitButton.Click();
    // request element information
    var message = driver.FindElement(By.Id("message"));
    var value = message.Text;
    Console.WriteLine($"message: {value}");
    // quit session
    driver.Quit();
  }

  public static void Test003()
  { // implicit wait
    // start session
    var options = new FirefoxOptions();
    options.AddArgument("-headless");
    var driver = new FirefoxDriver(options);
    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

    driver.Url = "https://www.selenium.dev/selenium/web/dynamic.html";
    driver.FindElement(By.Id("adder")).Click();

    IWebElement added = driver.FindElement(By.Id("box0"));
    var result = added.GetDomAttribute("class");
    Console.WriteLine($"result = {result}");
    driver.Quit();
  }

  public static void Test004()
  { // explicit
    // start session
    var options = new FirefoxOptions();
    options.AddArgument("-headless");
    var driver = new FirefoxDriver(options)
    {
      Url = "https://www.selenium.dev/selenium/web/dynamic.html"
    };
    IWebElement revealed = driver.FindElement(By.Id("revealed"));
    driver.FindElement(By.Id("reveal")).Click();

    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
    wait.Until(d => revealed.Displayed);

    revealed.SendKeys("Displayed");
    var result = revealed?.GetDomProperty("value");
    Console.WriteLine($"result = {result}");

    // quit session
    driver.Quit();
  }

  public static void RunCliLoop(WebDriverSession session)
  {
    while (true)
    {
      Console.Write("driver> ");
      var command = Console.ReadLine();
      if (command == "list")
      {
        session.ListOpenTabs();
      }
      else if (command != null && command.StartsWith("goto "))
      { 
        var url = command[5..].Trim();
        session.GotoNewTab(url);
      } 
      else if (command == "test001")
      {
        session.Test006();
      }
      else if (command == "quit" || command == "exit")
      {
        break;
      }
    }
  }

  public static void Test005()
  {
    using var session = new WebDriverSession();
    RunCliLoop(session);
  }
}

public class WebDriverSession : IDisposable
{
  private readonly FirefoxDriver driver;

  public WebDriverSession()
  {
    var options = new FirefoxOptions();
    options.AddArgument("-headless");
    driver = new FirefoxDriver(options);
    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
  }

  public void Dispose()
  {
    Console.WriteLine("driver session quit.");
    driver.Quit();
    GC.SuppressFinalize(this);
  }

  public void ListOpenTabs()
  {
    var windowsHandles = driver.WindowHandles;
    for (int i = 0; i < windowsHandles.Count; ++i)
    { 
      driver.SwitchTo().Window(windowsHandles[i]);
      var title = driver.Title;
      Console.WriteLine($"{i + 1}. {title}");
    }
  }

  public void GotoNewTab(string url)
  { 
    driver.SwitchTo().NewWindow(WindowType.Tab);
    driver.Navigate().GoToUrl(url);
  }

  public void Test006()
  { 
    // goto url
    driver.Navigate().GoToUrl("https://www.selenium.dev/selenium/web/web-form.html");
    // request browser information: title
    var title = driver.Title;
    Console.WriteLine($"title: {title}");
    // establish waiting strategy
    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);
    // find elements
    var textBox = driver.FindElement(By.Name("my-text"));
    var submitButton = driver.FindElement(By.TagName("button"));
    // take actions on elements
    textBox.SendKeys("Selenium");
    submitButton.Click();
    // request element information
    var message = driver.FindElement(By.Id("message"));
    var value = message.Text;
    Console.WriteLine($"message: {value}");
  }
}