using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Talpa_UITests;

public class Tests
{
    private IWebDriver driver;

    public IDictionary<string, object> vars { get; private set; }

    private IJavaScriptExecutor js;

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        js = (IJavaScriptExecutor)driver;
        vars = new Dictionary<string, object>();
    }

    [TearDown]
    protected void TearDown()
    {
        driver.Quit();
    }

    [Test]
    public void login_successfully()
    {
        driver.Navigate().GoToUrl("http://localhost:3000/");
        driver.Manage().Window.Size = new System.Drawing.Size(1094, 1032);
        driver.FindElement(By.Id("username")).SendKeys("manager@gmail.com");
        driver.FindElement(By.Id("password")).SendKeys("Password123");
        driver.FindElement(By.CssSelector(".c480bc568")).Click();
    }
}