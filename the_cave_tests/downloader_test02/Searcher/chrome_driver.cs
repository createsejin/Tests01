using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using HtmlAgilityPack;

namespace searcher;

class ChromeDriverManager
{
  private readonly ChromeDriver driver;
  public ChromeDriverManager()
  {
    driver = new();
    // 암묵적으로 지정된 시간만큼 기다려준다. 이것은 기본적으로 설정하는것이 이롭다.
    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);
  }

  public void Test001()
  {
    // 찾을 RJ number다.
    string search_rj = "RJ435409";
    driver.Navigate().GoToUrl("https://www.anime-sharing.com/search/");

    // XPath로 찾는 방법 -> 이게 가장 확실한 방법이긴 하다. 그러나 동적으로 폼이 변하는 페이지는 이걸로 찾기 힘들듯. 
    var search_input = driver.FindElement(
      By.XPath(@"/html/body/div[2]/div/div[3]/div/div[2]/div[2]/div/div/form/div/div/dl[1]/dd/ul/li[1]/input"));
    search_input.SendKeys(search_rj);

    // search submit button을 css selector로 찾는다.
    var search_button = driver.FindElement(
      By.CssSelector("#top > div.p-body > div > div.uix_contentWrapper > div.p-body-main >" +
        " div > div > form > div > dl > dd > div > div.formSubmitRow-controls > button"));
    // 클릭해준다.
    search_button.Click();

    // search 결과에 나오는 첫머리 RJ number를 표시해줄 element를 찾는다.
    var search_query_ele = driver.FindElement(By.CssSelector("#top > div.p-body > div > div.uix_contentWrapper >" +
      " div.p-body-header > div > div > div > div.p-title > h1 > a > em"));
    // element가 확실하게 표시될때까지 기다리기 위한 wait 객체다.
    WebDriverWait wait = new(driver, TimeSpan.FromSeconds(7))
    {
      // polling rate를 설정해준다. 설정한 시간대로 결과가 표시됐는지 루핑한다.
      PollingInterval = TimeSpan.FromMilliseconds(150),
    };
    // 위에서 지정한 element가 display될때까지 기다린다.
    wait.Until(d => search_query_ele.Displayed);
    // element가 완전히 로딩되면 그 element의 내용을 추출한다.
    string search_query_text = search_query_ele.Text;
    Console.WriteLine($"search query = {search_query_text}");
    // 그 내용을 추출해서 처음 목표한 RJ number와 일치하는지 검사한다.
    if (search_rj == search_query_text)
    {
      Console.WriteLine("searcher get results:");
    }
    else
    {
      Console.WriteLine("searcher no get result");
      return;
    }
    wait.Until(d => d.FindElement(
      By.XPath(@"//*[@id=""top""]/div[3]/div/div[3]/div[2]/div/div/div/div[1]/ol")).Displayed);
    // 현재 페이지의 HTML page source를 가져온다.
    string pageHtml = driver.PageSource;
    // HtmlAgilityPack의 HTML document 객체를 만든다.
    HtmlDocument html_doc = new();
    // string으로 부터 HTML을 파싱한다.
    html_doc.LoadHtml(pageHtml);
    // 지정된 xpath로 ol body의 node를 선택한다.
    var ol_node = html_doc.DocumentNode
      .SelectSingleNode(@"//*[@id=""top""]/div[3]/div/div[3]/div[2]/div/div/div/div[1]/ol");
    // 그 ol node의 li node들을 선택해준다.
    var li_nodes = ol_node.SelectNodes("li");
    // 얻은 li node들을 루핑
    foreach (var li in li_nodes)
    {
      // li node의 data-author 속성을 추출, 출력해준다.
      string author = li.GetAttributeValue("data-author", "");
      if (!string.IsNullOrEmpty(author))
      {
        // ol/li[1]/div/div/h3/a
        //    ^^---------------^
        // li node로부터 a node의 상대 경로로 찾는다. 
        var a_node = li.SelectSingleNode("div/div/h3/a");
        string link = new("");
        // a_node로부터 하이퍼 링크를 얻는다.
        if (a_node != null) link = a_node.GetAttributeValue("href", "");
        // 링크가 `/threads`로 시작하는 애들만 모아다가 출력해준다.
        if (!string.IsNullOrEmpty(link) && link.StartsWith("/threads"))
        {
          Console.WriteLine($"author = {author}");
          Console.WriteLine(link);
          Console.WriteLine();
        }
      }
    }
    driver.Quit();
  }
}