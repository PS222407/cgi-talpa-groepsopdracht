using BusinessLogicLayer.Interfaces.Repositories;
using BusinessLogicLayer.Interfaces.Services;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Services;

public class OutingService : IOutingService
{
    private readonly IOutingRepository _outingRepository;

    public OutingService(IOutingRepository outingRepository)
    {
        _outingRepository = outingRepository;
    }

    public Outing Create(Outing outing, int teamId)
    {
        if (outing == null)
        {
            throw new ArgumentOutOfRangeException("Outing can't be null");
        }

        return _outingRepository.Create(outing, teamId);
    }

    public Outing? GetById(int id)
    {
        Outing? outing = _outingRepository.GetById(id);

        if (outing == null)
        {
            throw new ArgumentNullException("Outing doesn't exist");
        }
         
        return _outingRepository.GetById(id);
    }

    public Outing? GetByIdWithVotes(int id)
    {
        return _outingRepository.GetByIdWithVotes(id);
    }

    public List<Outing> GetAll()
    {
        return _outingRepository.GetAll();
    }

    public List<Outing> GetAllComplete()
    {
        List<Outing> completeOutings = _outingRepository.GetAllComplete();

        return completeOutings.Where(completeOuting => completeOuting.DeadLine > DateTime.Now).ToList();
    }

    public bool Update(Outing newOuting)
    {
        if (newOuting == null)
        {
            throw new ArgumentOutOfRangeException("Outing can't be null");
        }

        Outing? outing = _outingRepository.GetById((int)newOuting.Id);

        if (outing == null)
        {
            throw new ArgumentNullException("Outing doesn't exist");
        }

        if (newOuting.Suggestions?.Count > 3)
        {
            return false;
        }

        return _outingRepository.Update(outing);
    }

    public bool Delete(int id)
    {
        Outing? outing = _outingRepository.GetById(id);

        if (outing == null)
        {
            throw new ArgumentNullException("Outing doesn't exist");
        }

        return _outingRepository.Delete(id);
    }

    public List<Outing> GetAllFromTeam(int teamId)
    {
        return _outingRepository.GetAllFromTeam(teamId);
    }

    public List<Outing> GetAllCompleteFromTeam(int teamId)
    {
        List<Outing> completeOutings = _outingRepository.GetAllCompleteFromTeam(teamId);

        return completeOutings.Where(completeOuting => completeOuting.DeadLine > DateTime.Now).ToList();
    }

    public bool UserHasVotedForOuting(string userId, int outingId)
    {
        return _outingRepository.UserHasVotedDatesForOuting(userId, outingId)
               || _outingRepository.UserHasVotedSuggestionForOuting(userId, outingId);
    }

    public bool Vote(string userId, int outingId, int suggestionId, List<int> votedDateIds)
    {
        Outing? outing = _outingRepository.GetById(outingId);

        if (outing == null)
        {
            throw new ArgumentNullException("Outing doesn't exist");
        }

        return !UserHasVotedForOuting(userId, outingId)
               && _outingRepository.Vote(userId, outingId, suggestionId, votedDateIds);
    }
    public bool ConfirmOuting(int id, int suggestionId, int outingDateId)
    {
        Outing? outing = _outingRepository.GetById(id);

        if (outing == null)
        {
            throw new ArgumentNullException("Outing doesn't exist");
        }

        return _outingRepository.ConfirmOuting(id, suggestionId, outingDateId);
    }

    public Outing? GetOutingByIdWithMostVotedDatesAndSuggestions(int id)
    {
        Outing? outing = _outingRepository.GetById(id);

        if (outing == null)
        {
            throw new ArgumentNullException("Outing doesn't exist");
        }

        return _outingRepository.GetOutingByIdWithMostVotedDatesAndSuggestions(id);
    }

    public List<Outing> GetAllConfirmedFromTeam(int teamId)
    {
        return _outingRepository.GetAllConfirmedFromTeam(teamId);
    }
}