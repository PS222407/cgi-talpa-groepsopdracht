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
        return _outingRepository.Create(outing, teamId);
    }

    public Outing? GetById(int id)
    {
        return _outingRepository.GetById(id);
    }

    public List<Outing> GetAll()
    {
        return _outingRepository.GetAll();
    }

    public bool Update(Outing outing)
    {
        return _outingRepository.Update(outing);
    }

    public bool Delete(int id)
    {
        return _outingRepository.Delete(id);
    }

    public List<Outing> GetAllFromTeam(int teamId)
    {
        return _outingRepository.GetAllFromTeam(teamId);
    }
}