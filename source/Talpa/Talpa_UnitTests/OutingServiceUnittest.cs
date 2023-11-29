using BusinessLogicLayer.Models;
using BusinessLogicLayer.Services;

namespace Talpa_UnitTests;

public class OutingServiceUnittest
{
    [Test]
    public void Create_ValidInput_Success()
    {
        Outing outing = new Outing
        {
            Id = 1,
            Name = "Test",
            ImageUrl = "/url",
            OutingDates = new List<OutingDate>
            {
                new OutingDate
                {
                    Id = 2,
                    Date = new DateTime(),
                }
            }
    };

        string userid = "user1";

        SuggestionTestRepository repository = new SuggestionTestRepository();
        SuggestionService suggestionService = new SuggestionService(repository);

        suggestionService.Create(suggestion, userid);

        Suggestion actual = repository.GetById(suggestion.Id); 
        Assert.AreEqual(suggestion.Name, actual.Name);
        Assert.AreEqual(suggestion.ImageUrl, actual.ImageUrl);
    }

    [Test]
    public void Create_AlreadyExists_Failure()
    {
        Suggestion suggestion = new Suggestion
        {
            Id = 1,
            Name = "Test",
            ImageUrl = "/url",
        };

        string userid = "user1";

        SuggestionTestRepository repository = new SuggestionTestRepository();
        repository.Add(suggestion, userid);
        SuggestionService suggestionService = new SuggestionService(repository);

        var actual = suggestionService.Create(suggestion, userid);
        
        Assert.IsNull(actual);
    }

    [Test]
    public void Create_NullValue_Failure()
    {
        Suggestion suggestion = null;

        string userid = "user1";

        SuggestionTestRepository repository = new SuggestionTestRepository();
        SuggestionService suggestionService = new SuggestionService(repository);

        void Create()
        {
            suggestionService.Create(suggestion, userid);
        }

        Assert.Throws<ArgumentOutOfRangeException>(Create);
    }

    [Test]
    public void Update_ValidInput_Success()
    {
        Suggestion suggestion = new Suggestion
        {
            Id = 1,
            Name = "Test",
            ImageUrl = "/url",
        };
        Suggestion newSuggestion = new Suggestion
        {
            Id = 1,
            Name = "Test2",
            ImageUrl = "/url2",
        };

        string userid = "user1";

        SuggestionTestRepository repository = new SuggestionTestRepository();
        repository.Add(suggestion, userid);
        SuggestionService suggestionService = new SuggestionService(repository);

        bool tryUpdate = suggestionService.Update(newSuggestion, userid);

        Assert.IsTrue(tryUpdate);
    }

    [Test]
    public void Update_DoesNotExist_Failure()
    {
        Suggestion suggestion = new Suggestion
        {
            Id = 1,
            Name = "Test",
            ImageUrl = "/url",
        };

        string userid = "user1";

        SuggestionTestRepository repository = new SuggestionTestRepository();
        SuggestionService suggestionService = new SuggestionService(repository);

        void Update()
        {
            suggestionService.Update(suggestion, userid);
        }

        Assert.Throws<ArgumentNullException>(Update);
    }

    [Test]
    public void Update_NullValue_Failure()
    {
        Suggestion suggestion = null;

        string userid = "user1";

        SuggestionTestRepository repository = new SuggestionTestRepository();
        SuggestionService suggestionService = new SuggestionService(repository);

        void Update()
        {
            suggestionService.Update(suggestion, userid);
        }

        Assert.Throws<ArgumentOutOfRangeException>(Update);
    }

    [Test]
    public void Update_UserIDIsDifferent_Failure()
    {
        Suggestion suggestion = new Suggestion
        {
            Id = 1,
            Name = "Test",
            ImageUrl = "/url",
        };

        string userid = "user1";

        SuggestionTestRepository repository = new SuggestionTestRepository();
        repository.Add(suggestion, userid);
        SuggestionService suggestionService = new SuggestionService(repository);

        bool tryUpdate = suggestionService.Update(suggestion, "user2");

        Assert.IsFalse(tryUpdate);
    }

    [Test]
    public void Delete_ValidInput_Success()
    {
        Suggestion suggestion = new Suggestion
        {
            Id = 1,
            Name = "Test",
            ImageUrl = "/url",
        };

        string userid = "user1";

        SuggestionTestRepository repository = new SuggestionTestRepository();
        repository.Add(suggestion, userid);
        SuggestionService suggestionService = new SuggestionService(repository);

        bool tryDelete = suggestionService.Delete(suggestion.Id, userid);

        Assert.IsTrue(tryDelete);
    }

    [Test]
    public void Delete_DoesNotExist_Success()
    {
        string userid = "user1";

        SuggestionTestRepository repository = new SuggestionTestRepository();
        SuggestionService suggestionService = new SuggestionService(repository);

        void Delete()
        {
            suggestionService.Delete(643746, userid);
        }

        Assert.Throws<ArgumentNullException>(Delete);
    }

    [Test]
    public void Delete_UserIDIsDifferent_Failure()
    {
        Suggestion suggestion = new Suggestion
        {
            Id = 1,
            Name = "Test",
            ImageUrl = "/url",
        };

        string userid = "user1";

        SuggestionTestRepository repository = new SuggestionTestRepository();
        repository.Add(suggestion, userid);
        SuggestionService suggestionService = new SuggestionService(repository);

        bool tryDelete = suggestionService.Delete(suggestion.Id, "user2");

        Assert.IsFalse(tryDelete);
    }


    [Test]
    public void GetById_ValidInput_Success()
    {
        Suggestion newSuggestion = new Suggestion
        {
            Id = 1,
            Name = "Test",
            ImageUrl = "/url",
        };

        string userid = "user1";

        SuggestionTestRepository repository = new SuggestionTestRepository();
        repository.Add(newSuggestion, userid);
        SuggestionService suggestionService = new SuggestionService(repository);

        Suggestion suggestion = suggestionService.GetById(newSuggestion.Id, userid);

        Assert.AreEqual(newSuggestion.Name, suggestion.Name);
        Assert.AreEqual(newSuggestion.ImageUrl, suggestion.ImageUrl);
    }

    [Test]
    public void GetById_DoesNotExist_Success()
    {
        string userid = "user1";

        SuggestionTestRepository repository = new SuggestionTestRepository();
        SuggestionService suggestionService = new SuggestionService(repository);

        void GetById()
        {
            suggestionService.GetById(643746, userid);
        }

        Assert.Throws<ArgumentNullException>(GetById);
    }

    [Test]
    public void GetById_UserIDIsDifferent_Failure()
    {
        Suggestion newSuggestion = new Suggestion
        {
            Id = 1,
            Name = "Test",
            ImageUrl = "/url",
        };

        string userid = "user1";

        SuggestionTestRepository repository = new SuggestionTestRepository();
        repository.Add(newSuggestion, userid);
        SuggestionService suggestionService = new SuggestionService(repository);

        Suggestion suggestion = suggestionService.GetById(newSuggestion.Id, "user2");

        Assert.IsNull(suggestion);
    }
}