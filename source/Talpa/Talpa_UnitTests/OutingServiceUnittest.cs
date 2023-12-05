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
                    Date = new DateTime(2023,12,6),
                }
            }
        };
        int teamId = 1;
        OutingTestRepository outingTestRepository = new OutingTestRepository();
        OutingService outingService = new OutingService(outingTestRepository);

        outingService.Create(outing, teamId);

        Outing actual = outingTestRepository.GetById((int)outing.Id);
        Assert.AreEqual(outing.Name, actual.Name);
        Assert.AreEqual(outing.ImageUrl, actual.ImageUrl);
        for (int i = 0; i < outing.OutingDates.Count; i++) 
        {
            OutingDate outingDate = outing.OutingDates[i];
            OutingDate actualDate = outing.OutingDates[i];
            Assert.AreEqual(outingDate.Date, actualDate.Date);
        }
    }

    [Test]
    public void Create_NullValue_Failure()
    {
        Outing outing = null;
        int teamid = 2;
        OutingTestRepository outingTestRepository = new OutingTestRepository();
        OutingService outingService = new OutingService(outingTestRepository);

        void Create()
        {
            outingService.Create(outing, teamid);
        }

        Assert.Throws<ArgumentOutOfRangeException>(Create);
    }

    [Test]
    public void Update_ValidInput_Success()
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
                    Date = new DateTime(2023,12,6),
                }
            }
        };
        Outing newOuting = new Outing
        {
            Id = 1,
            Name = "Test2",
            ImageUrl = "/url2",
            Suggestions = new List<Suggestion>
            {
                new Suggestion
                {
                    Name = "Test",
                    ImageUrl = "Url"
                },
                new Suggestion{
                    Name = "Test2",
                    ImageUrl = "Url2"
                },
                new Suggestion{
                    Name = "Test3",
                    ImageUrl = "Url3"
                },
            },
            DeadLine = new DateTime(2023, 12, 26, 18, 40, 20),
            OutingDates = new List<OutingDate>
            {
                new OutingDate
                {
                    Id = 2,
                    Date = new DateTime(2023,12,7),
                }
            }
        };
        int teamId = 1;
        OutingTestRepository outingTestRepository = new OutingTestRepository();
        outingTestRepository.Add(outing, teamId);
        OutingService outingService = new OutingService(outingTestRepository);

        bool tryUpdate = outingService.Update(newOuting);

        Assert.IsTrue(tryUpdate);
    }

    [Test]
    public void Update_MoreThan3Suggestions_Failure()
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
                    Date = new DateTime(2023,12,6),
                }
            }
        };
        Outing newOuting = new Outing
        {
            Id = 1,
            Name = "Test2",
            ImageUrl = "/url2",
            Suggestions = new List<Suggestion>
            {
                new Suggestion
                {
                    Name = "Test",
                    ImageUrl = "Url"
                },
                new Suggestion{
                    Name = "Test2",
                    ImageUrl = "Url2"
                },
                new Suggestion{
                    Name = "Test3",
                    ImageUrl = "Url3"
                },
                new Suggestion{
                    Name = "Test4",
                    ImageUrl = "Url4"
                },
            },
            DeadLine = new DateTime(2023, 12, 26, 18, 40, 20),
            OutingDates = new List<OutingDate>
            {
                new OutingDate
                {
                    Id = 2,
                    Date = new DateTime(2023,12,7),
                }
            }
        };
        int teamId = 1;
        OutingTestRepository outingTestRepository = new OutingTestRepository();
        outingTestRepository.Add(outing, teamId);
        OutingService outingService = new OutingService(outingTestRepository);

        bool tryUpdate = outingService.Update(newOuting);

        Assert.IsFalse(tryUpdate);
    }

    [Test]
    public void Update_DoesNotExist_Failure()
    {
        Outing outing = new Outing
        {
            Id = 1,
            Name = "Test",
            ImageUrl = "/url",
            Suggestions = new List<Suggestion>
            {
                new Suggestion
                {
                    Name = "Test",
                    ImageUrl = "Url"
                },
                new Suggestion{
                    Name = "Test2",
                    ImageUrl = "Url2"
                },
                new Suggestion{
                    Name = "Test3",
                    ImageUrl = "Url3"
                },
                new Suggestion{
                    Name = "Test4",
                    ImageUrl = "Url4"
                },
            },
            DeadLine = new DateTime(2023, 12, 26, 18, 40, 20),
            OutingDates = new List<OutingDate>
            {
                new OutingDate
                {
                    Id = 2,
                    Date = new DateTime(2023,12,6),
                }
            }
        };
        int teamId = 1;
        OutingTestRepository outingTestRepository = new OutingTestRepository();
        OutingService outingService = new OutingService(outingTestRepository);

        void Update()
        {
            outingService.Update(outing);
        }

        Assert.Throws<ArgumentNullException>(Update);
    }

    [Test]
    public void Update_NullValue_Failure()
    {
        Outing outing = null;
        int teamId = 1;
        OutingTestRepository outingTestRepository = new OutingTestRepository();
        OutingService outingService = new OutingService(outingTestRepository);

        void Update()
        {
            outingService.Update(outing);
        }

        Assert.Throws<ArgumentOutOfRangeException>(Update);
    }

    [Test]
    public void Delete_ValidInput_Success()
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
                    Date = new DateTime(2023,12,6),
                }
            }
        };
        int teamId = 1;
        OutingTestRepository outingTestRepository = new OutingTestRepository();
        outingTestRepository.Add(outing, teamId);
        OutingService outingService = new OutingService(outingTestRepository);

        bool tryDelete = outingService.Delete((int)outing.Id);

        Assert.IsTrue(tryDelete);
    }

    [Test]
    public void Delete_DoesNotExist_Success()
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
                    Date = new DateTime(2023,12,6),
                }
            }
        };
        int teamId = 1;
        OutingTestRepository outingTestRepository = new OutingTestRepository();
        outingTestRepository.Add(outing, teamId);
        OutingService outingService = new OutingService(outingTestRepository);

        void Delete()
        {
            outingService.Delete(283746);
        }

        Assert.Throws<ArgumentNullException>(Delete);
    }

    [Test]
    public void GetById_ValidInput_Success()
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
                    Date = new DateTime(2023,12,6),
                }
            }
        };
        int teamId = 1;
        OutingTestRepository outingTestRepository = new OutingTestRepository();
        outingTestRepository.Add(outing, teamId);
        OutingService outingService = new OutingService(outingTestRepository);

        Outing actual = outingService.GetById((int)outing.Id);

        Assert.AreEqual(outing.Name, actual.Name);
        Assert.AreEqual(outing.ImageUrl, actual.ImageUrl);
        for (int i = 0; i < outing.OutingDates.Count; i++)
        {
            OutingDate outingDate = outing.OutingDates[i];
            OutingDate actualDate = outing.OutingDates[i];
            Assert.AreEqual(outingDate.Date, actualDate.Date);
        }
    }

    [Test]
    public void GetById_DoesNotExist_Success()
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
                    Date = new DateTime(2023,12,6),
                }
            }
        };
        int teamId = 1;
        OutingTestRepository outingTestRepository = new OutingTestRepository();
        outingTestRepository.Add(outing, teamId);
        OutingService outingService = new OutingService(outingTestRepository);

        void GetById()
        {
            outingService.GetById(643746);
        }

        Assert.Throws<ArgumentNullException>(GetById);
    }

    [Test]
    public void UserHasVotedForOuting_UserVoted_True()
    {
        OutingTestRepository outingTestRepository = new OutingTestRepository();
        outingTestRepository.Vote("user1", 2, 3, new List<int>
        {
            1,
            2
        });
        OutingService outingService = new OutingService(outingTestRepository);

        bool HasVoted = outingService.UserHasVotedForOuting("user1", 2);

        Assert.IsTrue(HasVoted);
    }

    [Test]
    public void UserHasVotedForOuting_UserHasNotVoted_False()
    {
        OutingTestRepository outingTestRepository = new OutingTestRepository();
        OutingService outingService = new OutingService(outingTestRepository);

        bool HasVoted = outingService.UserHasVotedForOuting("user1", 2);

        Assert.IsFalse(HasVoted);
    }

    [Test]
    public void Vote_ValidInput_Success()
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
                    Date = new DateTime(2023,12,6),
                }
            }
        };
        int teamId = 2;
        OutingTestRepository outingTestRepository = new OutingTestRepository();
        outingTestRepository.Add(outing, teamId);
        OutingService outingService = new OutingService(outingTestRepository);

        bool Vote = outingService.Vote("user1", 1, 3, new List<int>
        {
            1,
            2
        });

        Assert.IsTrue(Vote);
    }

    [Test]
    public void Vote_HasAlreadyVoted_Failure()
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
                    Date = new DateTime(2023,12,6),
                }
            }
        };
        int teamId = 2;
        OutingTestRepository outingTestRepository = new OutingTestRepository();
        outingTestRepository.Add(outing, teamId);
        outingTestRepository.Vote("user1", 1, 3, new List<int>
        {
            1,
            2
        });
        OutingService outingService = new OutingService(outingTestRepository);

        bool Vote = outingService.Vote("user1", 1, 3, new List<int>
        {
            1,
            2
        });

        Assert.IsFalse(Vote);
    }
    
    [Test]
    public void Vote_InvalidInput_Failure()
    {
        Outing outing = null;
        int teamId = 2;
        OutingTestRepository outingTestRepository = new OutingTestRepository();
        OutingService outingService = new OutingService(outingTestRepository);

        void Vote()
        {
            outingService.Vote("user1", 1, 3, new List<int> { 1, 2 });
        }

        Assert.Throws<ArgumentNullException>(Vote);
    }
}