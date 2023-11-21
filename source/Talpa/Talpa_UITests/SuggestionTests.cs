using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Talpa_UITests;

[TestFixture]
public class SuggestionTests
{
    // private IWebDriver driver;
    //
    // public IDictionary<string, object> vars { get; private set; }
    //
    // private IJavaScriptExecutor js;
    //
    // [SetUp]
    // public void SetUp()
    // {
    //     driver = new ChromeDriver();
    //     js = (IJavaScriptExecutor)driver;
    //     vars = new Dictionary<string, object>();
    //     
    //     LoginAsManager();
    // }
    //
    // [TearDown]
    // protected void TearDown()
    // {
    //     driver.Quit();
    // }
    //
    // private void LoginAsManager()
    // {
    //     driver.Navigate().GoToUrl("http://localhost:3000/");
    //     driver.Manage().Window.Size = new System.Drawing.Size(1094, 1032);
    //     driver.FindElement(By.Id("username")).SendKeys("manager@gmail.com");
    //     driver.FindElement(By.Id("password")).SendKeys("Password123");
    //     driver.FindElement(By.CssSelector(".c480bc568")).Click();
    // }
    //
    // [Test]
    // public void create_suggestion_successfully()
    // {
    //     driver.Navigate().GoToUrl("http://localhost:3000/");
    //     driver.Manage().Window.Size = new System.Drawing.Size(1094, 1032);
    //     driver.FindElement(By.CssSelector(".nav-item:nth-child(3) > .nav-link")).Click();
    //     driver.FindElement(By.LinkText("Create New")).Click();
    //     driver.FindElement(By.Id("Name")).Click();
    //     driver.FindElement(By.Id("Name")).SendKeys("Schoonmaken");
    //     driver.FindElement(By.Id("Description")).SendKeys("Heerlijk dagje schoonmaken met zijn allen!");
    //     js.ExecuteScript("window.scrollTo(0,0)");
    //     driver.FindElement(By.CssSelector(".select2-search__field")).SendKeys("Saai,");
    //     driver.FindElement(By.Id("Image")).SendKeys("/images/sample.jpg");
    //     driver.FindElement(By.CssSelector(".btn-primary")).Click();
    //     
    //     bool verifyFlashMessage = driver.FindElement(By.Id("flashmessage")).Text
    //         == "Item successfully created";
    //     
    //     Assert.That(verifyFlashMessage, Is.True);
    // }
}