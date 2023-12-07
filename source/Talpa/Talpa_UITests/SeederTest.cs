namespace Talpa_UITests;

public class SeederTest
{
    [Test]
    public void seed_successfully()
    {
        new DatabaseSeederService().Seed();
    }
}