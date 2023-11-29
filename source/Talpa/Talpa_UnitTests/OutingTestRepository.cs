using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Interfaces.Repositories;
using BusinessLogicLayer.Models;
using System.Reflection.Metadata.Ecma335;

namespace Talpa_UnitTests;

public class OutingTestRepository : IOutingRepository
{
    private readonly List<Outing> outings = new List<Outing>();

    public void Add(Outing outing, int teamId)
    {
        outing.TeamId = teamId;

        outings.Add(outing);
    }

    public bool ConfirmOuting(int id, int suggestionId, int outingDateId)
    {
        Outing? outing = outings.FirstOrDefault(o => o.Id == id);

        outing.ConfirmedOutingDateId = outingDateId;
        outing.ConfirmedSuggestionId = suggestionId;

        return true;
    }

    public Outing Create(Outing outing, int teamId)
    {
        outing.TeamId = teamId;
        
        outings.Add(outing);

        return outing;
    }

    public bool Delete(int id)
    {
        Outing? outing = outings.FirstOrDefault(o => o.Id == id);

        outings.Remove(outing);

        return true;
    }
    public Outing? GetById(int id)
    {
        return outings?
            .FirstOrDefault(o => o.Id == id);
    }
    public bool Update(Outing outing)
    {
        Outing? outingDb = outings
        .FirstOrDefault(o => o.Id == outing.Id);

        outingDb.Name = outing.Name;
        outingDb.Suggestions = outing.Suggestions;
        outingDb.OutingDates = outing.OutingDates;
        outingDb.DeadLine = outing.DeadLine;
        outingDb.ImageUrl = outing.ImageUrl;

        return true;
    }

    public List<Outing> GetAll()
    {
        throw new NotImplementedException();
    }

    public List<Outing> GetAllComplete()
    {
        throw new NotImplementedException();
    }

    public List<Outing> GetAllCompleteFromTeam(int teamId)
    {
        throw new NotImplementedException();
    }

    public List<Outing> GetAllConfirmedFromTeam(int teamId)
    {
        throw new NotImplementedException();
    }

    public List<Outing> GetAllFromTeam(int teamId)
    {
        throw new NotImplementedException();
    }
    public Outing? GetByIdWithVotes(int id)
    {
        throw new NotImplementedException();
    }

    public Outing? GetOutingByIdWithMostVotedDatesAndSuggestions(int id)
    {
        throw new NotImplementedException();
    }

    public bool UserHasVotedDatesForOuting(string userId, int outingId)
    {
        throw new NotImplementedException();
    }

    public bool UserHasVotedSuggestionForOuting(string userId, int outingId)
    {
        throw new NotImplementedException();
    }

    public bool Vote(string userId, int outingId, int suggestionId, List<int> votedDateIds)
    {
        throw new NotImplementedException();
    }
}