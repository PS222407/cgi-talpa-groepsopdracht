using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Talpa_UITests;

[TestFixture]
public class OutingTests
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
    public void create_outing_successfully()
    {
        _seeder.Seed();

        driver.Navigate().GoToUrl("http://localhost:3000/");
        driver.Manage().Window.Size = new System.Drawing.Size(974, 1032);
        driver.FindElement(By.CssSelector(".nav-item:nth-child(1) > .nav-link")).Click();
        driver.FindElement(By.LinkText("Create New")).Click();
        driver.FindElement(By.Id("Name")).Click();
        driver.FindElement(By.Id("Name")).SendKeys("New outing");
        driver.FindElement(By.Id("Image")).SendKeys(@"C:\images\sample.jpg");
        driver.FindElement(By.CssSelector(".month:nth-child(12)")).Click();
        driver.FindElement(By.CssSelector("tr:nth-child(4) > .day:nth-child(2)")).Click();
        driver.FindElement(By.CssSelector("tr:nth-child(4) > .day:nth-child(4)")).Click();
        driver.FindElement(By.CssSelector("tr:nth-child(4) > .day:nth-child(5)")).Click();
        driver.FindElement(By.CssSelector("tr:nth-child(4) > .day:nth-child(6)")).Click();
        driver.FindElement(By.CssSelector(".btn-primary")).Click();

        bool verifyFlashMessage = driver.FindElement(By.Id("flashmessage")).Text
                                  == "Item successfully created";

        Assert.That(verifyFlashMessage, Is.True);
    }

    [Test]
    public void create_outing_fails()
    {
        _seeder.Seed();

        driver.Navigate().GoToUrl("http://localhost:3000/");
        driver.Manage().Window.Size = new System.Drawing.Size(974, 1032);
        driver.FindElement(By.CssSelector(".nav-item:nth-child(1) > .nav-link")).Click();
        driver.FindElement(By.LinkText("Create New")).Click();
        driver.FindElement(By.Id("Image")).SendKeys(@"C:\images\sample.jpg");
        driver.FindElement(By.CssSelector(".month:nth-child(12)")).Click();
        driver.FindElement(By.CssSelector("tr:nth-child(4) > .day:nth-child(2)")).Click();
        driver.FindElement(By.CssSelector("tr:nth-child(4) > .day:nth-child(4)")).Click();
        driver.FindElement(By.CssSelector("tr:nth-child(4) > .day:nth-child(5)")).Click();
        driver.FindElement(By.CssSelector("tr:nth-child(4) > .day:nth-child(6)")).Click();
        driver.FindElement(By.CssSelector(".btn-primary")).Click();

        string message = driver.FindElement(By.CssSelector("[data-valmsg-for=\"Name\"]")).Text;
        bool verifyFlashMessage = message == "The Name field is required";

        Assert.That(verifyFlashMessage, Is.True);
    }

    [Test]
    public void edit_outing_successfully()
    {
        _seeder.Seed();

        driver.Navigate().GoToUrl("http://localhost:3000/");
        driver.Manage().Window.Size = new System.Drawing.Size(974, 1032);
        driver.FindElement(By.CssSelector(".nav-item:nth-child(1) > .nav-link")).Click();
        driver.FindElement(By.LinkText("Edit")).Click();
        driver.FindElement(By.Id("Name")).Click();
        driver.FindElement(By.Id("Name")).SendKeys("Winter uitje edit");
        driver.FindElement(By.Id("Image")).SendKeys(@"C:\images\sample.jpg");
        driver.FindElement(By.CssSelector(".row")).Click();
        driver.FindElement(By.CssSelector(".month:nth-child(6)")).Click();
        driver.FindElement(By.CssSelector("tr:nth-child(3) > .day:nth-child(2)")).Click();
        driver.FindElement(By.CssSelector("tr:nth-child(3) > .day:nth-child(4)")).Click();
        driver.FindElement(By.CssSelector("tr:nth-child(3) > .day:nth-child(5)")).Click();
        driver.FindElement(By.CssSelector("tr:nth-child(3) > .day:nth-child(6)")).Click();
        driver.FindElement(By.Id("Name")).Click();
        driver.FindElement(By.Id("Name")).SendKeys(Keys.Enter);

        bool verifyFlashMessage = driver.FindElement(By.Id("flashmessage")).Text
                                  == "Item successfully updated";

        Assert.That(verifyFlashMessage, Is.True);
    }
    
    [Test]
    public void edit_outing_fails()
    {
        _seeder.Seed();

        driver.Navigate().GoToUrl("http://localhost:3000/");
        driver.Manage().Window.Size = new System.Drawing.Size(974, 1032);
        driver.FindElement(By.CssSelector(".nav-item:nth-child(1) > .nav-link")).Click();
        driver.FindElement(By.LinkText("Edit")).Click();
        driver.FindElement(By.Id("Name")).Click();
        driver.FindElement(By.Id("Name")).Clear();
        driver.FindElement(By.Id("Image")).SendKeys(@"C:\images\sample.jpg");
        driver.FindElement(By.CssSelector(".row")).Click();
        driver.FindElement(By.CssSelector(".month:nth-child(6)")).Click();
        driver.FindElement(By.CssSelector("tr:nth-child(3) > .day:nth-child(2)")).Click();
        driver.FindElement(By.CssSelector("tr:nth-child(3) > .day:nth-child(4)")).Click();
        driver.FindElement(By.CssSelector("tr:nth-child(3) > .day:nth-child(5)")).Click();
        driver.FindElement(By.CssSelector("tr:nth-child(3) > .day:nth-child(6)")).Click();
        driver.FindElement(By.Id("Name")).Click();
        driver.FindElement(By.Id("Name")).SendKeys(Keys.Enter);

        string message = driver.FindElement(By.CssSelector("[data-valmsg-for=\"Name\"]")).Text;
        bool verifyFlashMessage = message == "The Name field is required";

        Assert.That(verifyFlashMessage, Is.True);
    }
    
    [Test]
    public void confirm_outing_successfully()
    {
        _seeder.Seed();
        _seeder.AddDateVotes("auth0|6511c2f00a3128c2f449b037", 8);
        _seeder.AddDateVotes("auth0|6511c2f00a3128c2f449b037", 10);
        _seeder.AddSuggestionVotes("auth0|6511c2f00a3128c2f449b037", 4, 3);
        
        driver.Navigate().GoToUrl("http://localhost:3000/");
        driver.Manage().Window.Size = new System.Drawing.Size(945, 1012);
        driver.FindElement(By.CssSelector(".nav-item:nth-child(1) > .nav-link")).Click();
        driver.FindElement(By.CssSelector("tr:nth-child(3) .btn:nth-child(2)")).Click();
        driver.FindElement(By.CssSelector(".suggestionCard")).Click();
        driver.FindElement(By.CssSelector("label:nth-child(3) > .outingDateCard")).Click();
        driver.FindElement(By.CssSelector(".btn-primary")).Click();

        bool verifyFlashMessage = driver.FindElement(By.Id("flashmessage")).Text == "Item successfully updated";

        Assert.That(verifyFlashMessage, Is.True);
    }
    
    [Test]
    public void delete_outing_successfully()
    {
        _seeder.Seed();

        driver.Navigate().GoToUrl("http://localhost:3000/");
        driver.Manage().Window.Size = new System.Drawing.Size(945, 1012);
        driver.FindElement(By.CssSelector(".nav-item:nth-child(1) > .nav-link")).Click();
        driver.FindElement(By.CssSelector("tr:nth-child(3) .btn-primary")).Click();
        driver.FindElement(By.CssSelector(".btn-primary")).Click();

        bool verifyFlashMessage = driver.FindElement(By.Id("flashmessage")).Text == "Item successfully deleted";

        Assert.That(verifyFlashMessage, Is.True);
    }
}