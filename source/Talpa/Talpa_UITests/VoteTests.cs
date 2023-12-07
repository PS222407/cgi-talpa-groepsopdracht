using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Talpa_UITests;

[TestFixture]
public class VoteTests
{
    private readonly DatabaseSeederService _seeder = new();

    private IWebDriver driver;

    public IDictionary<string, object> vars { get; private set; }

    private IJavaScriptExecutor js;

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        js = (IJavaScriptExecutor)driver;
        vars = new Dictionary<string, object>();

        LoginAsManager();
    }

    [TearDown]
    protected void TearDown()
    {
        driver.Quit();
    }

    private void LoginAsManager()
    {
        driver.Navigate().GoToUrl("http://localhost:3000/");
        driver.Manage().Window.Size = new System.Drawing.Size(1094, 1032);
        driver.FindElement(By.Id("username")).SendKeys("manager@gmail.com");
        driver.FindElement(By.Id("password")).SendKeys("Password123");
        driver.FindElement(By.XPath("(//button[@type='submit'])[last()]")).Click();
    }
    
    [Test]
    public void vote_outing_successfully()
    {
        _seeder.Seed();

        driver.Navigate().GoToUrl("http://localhost:3000/");
        driver.Manage().Window.Size = new System.Drawing.Size(945, 945);
        driver.FindElement(By.CssSelector(".nav-item:nth-child(2) > .nav-link")).Click();
        driver.FindElement(By.CssSelector(".card-body")).Click();
        driver.FindElement(By.CssSelector(".card-frame:nth-child(2) .card-body")).Click();
        driver.FindElement(By.CssSelector("#vote-suggestion-form")).Submit();
        driver.FindElement(By.CssSelector("label:nth-child(4) > .vote-date-card")).Click();
        driver.FindElement(By.CssSelector("label:nth-child(2) > .vote-date-card")).Click();
        driver.FindElement(By.CssSelector("#vote-date-form")).Submit();

        bool verifyFlashMessage = driver.FindElement(By.Id("flashmessage")).Text == "Vote successful";

        Assert.That(verifyFlashMessage, Is.True);
    }
    
    [Test]
    public void vote_outing_twice_fails()
    {
        _seeder.Seed();

        driver.Navigate().GoToUrl("http://localhost:3000/");
        driver.Manage().Window.Size = new System.Drawing.Size(945, 945);
        driver.FindElement(By.CssSelector(".nav-item:nth-child(2) > .nav-link")).Click();
        driver.FindElement(By.CssSelector(".card-body")).Click();
        driver.FindElement(By.CssSelector(".card-frame:nth-child(2) .card-body")).Click();
        driver.FindElement(By.CssSelector("#vote-suggestion-form")).Submit();
        driver.FindElement(By.CssSelector("label:nth-child(4) > .vote-date-card")).Click();
        driver.FindElement(By.CssSelector("label:nth-child(2) > .vote-date-card")).Click();
        driver.FindElement(By.CssSelector("#vote-date-form")).Submit();

        driver.Navigate().GoToUrl("http://localhost:3000/");
        driver.Manage().Window.Size = new System.Drawing.Size(945, 945);
        driver.FindElement(By.CssSelector(".nav-item:nth-child(2) > .nav-link")).Click();
        driver.FindElement(By.CssSelector(".card-body")).Click();

        bool verifyFlashMessage = driver.FindElement(By.Id("flashmessage")).Text == "You have already voted for this outing";

        Assert.That(verifyFlashMessage, Is.True);
    }
}