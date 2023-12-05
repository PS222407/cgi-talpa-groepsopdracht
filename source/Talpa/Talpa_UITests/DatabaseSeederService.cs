using BusinessLogicLayer.Models;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Talpa_UITests;

public class DatabaseSeederService
{
    private readonly DataContext _dataContext;

    private const string EmployeeId = "auth0|65114a310a3128c2f4492e9f";

    private const string ManagerId = "auth0|6511c2f00a3128c2f449b037";

    private const string AdminId = "auth0|6511496df18c062bb839864a";

    public DatabaseSeederService()
    {
        string webProjectDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "Talpa_10_WebApp"));

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(webProjectDirectory) // Adjust the path accordingly
            .AddJsonFile("appsettings.Development.json")
            .Build();

        string connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()
            .UseMySql(
                connectionString,
                new MySqlServerVersion(new Version(10, 4, 22)), // Edit this to your SQL server version.
                mySqlOptions => mySqlOptions.MigrationsAssembly("Talpa_30_DataAccessLayer")
            )
            .Options;

        _dataContext = new DataContext(options);
    }

    public void Seed()
    {
        string[] tables =
        {
            "DateVotes",
            "OutingDates",
            "Outings",
            "OutingSuggestion",
            "Restrictions",
            "RestrictionSuggestion",
            "SuggestionDates",
            "Suggestions",
            "SuggestionVote",
            "Teams",
            "Appearances",
        };

        _dataContext.Database.ExecuteSqlRaw("SET FOREIGN_KEY_CHECKS=0;");
        foreach (string table in tables)
        {
            _dataContext.Database.ExecuteSqlRaw($"DELETE FROM {table};");
            _dataContext.Database.ExecuteSqlRaw($"ALTER TABLE {table} AUTO_INCREMENT = 1;");
        }

        _dataContext.Database.ExecuteSqlRaw("SET FOREIGN_KEY_CHECKS=1;");

        _dataContext.Teams.Add(new Team { Id = 1, Name = "Team 1" });
        _dataContext.SaveChanges();

        _dataContext.Restrictions.AddRange(
            new Restriction { Id = 1, Name = "Beperking 1" },
            new Restriction { Id = 2, Name = "Beperking 2" },
            new Restriction { Id = 3, Name = "Beperking 3" }
        );
        _dataContext.SaveChanges();

        _dataContext.Suggestions.AddRange(
            new Suggestion
            {
                Id = 1, Name = "Bowlen", UserId = ManagerId, Description = "Bowlen in de plaatselijke bowlingbaan", ImageUrl = "/uploads/images/870bc7ff-ff94-4083-adde-c8d5f0c8cd0a.jpg"
            },
            new Suggestion { Id = 2, Name = "Boogschieten", UserId = EmployeeId, Description = "Lekker boogschieten", ImageUrl = null },
            new Suggestion
            {
                Id = 3, Name = "Fietsen", UserId = ManagerId, Description = "Fietsen door de bossen van de Veluwe", ImageUrl = "/uploads/images/340dc5ec-01f4-4219-ac41-da44397ea43b.jpg"
            },
            new Suggestion { Id = 4, Name = "Schaatsen", UserId = ManagerId, Description = "Schaatsen op de ijsbaan", ImageUrl = "/uploads/images/9ed7cd9a-b00c-42bd-a564-08d98a79859e.jpg" },
            new Suggestion
            {
                Id = 5, Name = "Poolen", UserId = ManagerId, Description = "Een gezellig potje poolen bij de The Rex Snooker en pool Club",
                ImageUrl = "/uploads/images/f0a2a2d0-25d1-490d-8bcf-a51ee1c2bbfe.jpg"
            }
        );
        _dataContext.SaveChanges();

        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `RestrictionSuggestion` (`RestrictionsId`, `SuggestionsId`) VALUES (1, 1);");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `RestrictionSuggestion` (`RestrictionsId`, `SuggestionsId`) VALUES (2, 1);");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `RestrictionSuggestion` (`RestrictionsId`, `SuggestionsId`) VALUES (1, 2);");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `RestrictionSuggestion` (`RestrictionsId`, `SuggestionsId`) VALUES (2, 2);");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `RestrictionSuggestion` (`RestrictionsId`, `SuggestionsId`) VALUES (3, 2);");
        _dataContext.SaveChanges();

        _dataContext.Outings.AddRange(
            new Outing
            {
                Id = 1, Name = "Winter uitje", TeamId = 1, DeadLine = DateTime.Now.AddDays(7), ConfirmedSuggestionId = 1, ConfirmedOutingDateId = 1,
                ImageUrl = "/uploads/images/70db9ffc-0411-4347-bab1-f01d4ff75e8d.jpg"
            },
            new Outing
            {
                Id = 2, Name = "Tussendoor uitje", TeamId = 1, DeadLine = DateTime.Now.AddDays(7), ConfirmedSuggestionId = 2, ConfirmedOutingDateId = 6,
                ImageUrl = "/uploads/images/870bc7ff-ff94-4083-adde-c8d5f0c8cd0a.jpg"
            },
            new Outing
            {
                Id = 3, Name = "25-jarig bestaansuitje", TeamId = 1, DeadLine = DateTime.Now.AddDays(7), ConfirmedSuggestionId = null, ConfirmedOutingDateId = null,
                ImageUrl = "/uploads/images/01dd833a-a714-4c9e-9693-25c75e296bde.png"
            }
        );
        _dataContext.SaveChanges();

        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `OutingDates` (`OutingId`, `Date`) VALUES (1, '{DateTime.Now.AddDays(28):yyyy-MM-dd HH:mm:ss}');");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `OutingDates` (`OutingId`, `Date`) VALUES (1, '{DateTime.Now.AddDays(30):yyyy-MM-dd HH:mm:ss}');");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `OutingDates` (`OutingId`, `Date`) VALUES (1, '{DateTime.Now.AddDays(31):yyyy-MM-dd HH:mm:ss}');");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `OutingDates` (`OutingId`, `Date`) VALUES (2, '{DateTime.Now.AddDays(32):yyyy-MM-dd HH:mm:ss}');");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `OutingDates` (`OutingId`, `Date`) VALUES (2, '{DateTime.Now.AddDays(28):yyyy-MM-dd HH:mm:ss}');");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `OutingDates` (`OutingId`, `Date`) VALUES (2, '{DateTime.Now.AddDays(30):yyyy-MM-dd HH:mm:ss}');");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `OutingDates` (`OutingId`, `Date`) VALUES (2, '{DateTime.Now.AddDays(31):yyyy-MM-dd HH:mm:ss}');");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `OutingDates` (`OutingId`, `Date`) VALUES (3, '{DateTime.Now.AddDays(32):yyyy-MM-dd HH:mm:ss}');");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `OutingDates` (`OutingId`, `Date`) VALUES (3, '{DateTime.Now.AddDays(28):yyyy-MM-dd HH:mm:ss}');");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `OutingDates` (`OutingId`, `Date`) VALUES (3, '{DateTime.Now.AddDays(30):yyyy-MM-dd HH:mm:ss}');");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `OutingDates` (`OutingId`, `Date`) VALUES (3, '{DateTime.Now.AddDays(31):yyyy-MM-dd HH:mm:ss}');");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `OutingDates` (`OutingId`, `Date`) VALUES (3, '{DateTime.Now.AddDays(32):yyyy-MM-dd HH:mm:ss}');");
        _dataContext.SaveChanges();

        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `OutingSuggestion` (`OutingId`, `SuggestionsId`) VALUES (1, 1);");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `OutingSuggestion` (`OutingId`, `SuggestionsId`) VALUES (1, 2);");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `OutingSuggestion` (`OutingId`, `SuggestionsId`) VALUES (1, 3);");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `OutingSuggestion` (`OutingId`, `SuggestionsId`) VALUES (2, 2);");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `OutingSuggestion` (`OutingId`, `SuggestionsId`) VALUES (2, 3);");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `OutingSuggestion` (`OutingId`, `SuggestionsId`) VALUES (2, 4);");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `OutingSuggestion` (`OutingId`, `SuggestionsId`) VALUES (3, 3);");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `OutingSuggestion` (`OutingId`, `SuggestionsId`) VALUES (3, 4);");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `OutingSuggestion` (`OutingId`, `SuggestionsId`) VALUES (3, 5);");
        _dataContext.SaveChanges();

        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `DateVotes` (`UserId`, `OutingDateId`) VALUES ('{ManagerId}', 4);");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `DateVotes` (`UserId`, `OutingDateId`) VALUES ('{AdminId}', 4);");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `DateVotes` (`UserId`, `OutingDateId`) VALUES ('{EmployeeId}', 4);");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `DateVotes` (`UserId`, `OutingDateId`) VALUES ('{EmployeeId}', 5);");
        _dataContext.SaveChanges();

        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `SuggestionVote` (`UserId`, `SuggestionId`, `OutingId`) VALUES ('{ManagerId}', 1, 1);");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `SuggestionVote` (`UserId`, `SuggestionId`, `OutingId`) VALUES ('{AdminId}', 1, 1);");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `SuggestionVote` (`UserId`, `SuggestionId`, `OutingId`) VALUES ('{EmployeeId}', 1, 1);");
        _dataContext.Database.ExecuteSqlRaw($"INSERT INTO `SuggestionVote` (`UserId`, `SuggestionId`, `OutingId`) VALUES ('{EmployeeId}', 2, 1);");
        _dataContext.SaveChanges();
    }
}