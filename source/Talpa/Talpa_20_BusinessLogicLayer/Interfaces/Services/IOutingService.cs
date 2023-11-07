using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces.Services;

public interface IOutingService
{
    public Outing Create(Outing outing, int teamId);

    public Outing? GetById(int id);
    
    public Outing? GetByIdWithVotes(int id);

    public List<Outing> GetAll();

    public List<Outing> GetAllComplete();

    public bool Update(Outing outing);

    public bool Delete(int id);

    public List<Outing> GetAllFromTeam(int teamId);

    public List<Outing> GetAllCompleteFromTeam(int teamId);

    public bool UserHasVotedForOuting(string userId, int outingId);
    
    public bool Vote(string userId, int outingId, int suggestionId, List<int> votedDateIds);
}