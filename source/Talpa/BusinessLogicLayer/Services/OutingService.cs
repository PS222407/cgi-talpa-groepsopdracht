using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Dtos;
using DataAccessLayer.Interfaces;

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
        OutingDto outingDto = _outingRepository.Create(new OutingDto { Name = outing.Name }, teamId);

        return new Outing{Id = outingDto.Id, Name = outingDto.Name};
    }

    public Outing? GetById(int id)
    {
        OutingDto? outingDto = _outingRepository.GetById(id);
        if (outingDto == null)
        {
            return null;
        }

        return new Outing{Id = outingDto.Id, Name = outingDto.Name};
    }

    public List<Outing> GetAll()
    {
        return _outingRepository.GetAll().Select(outingDto => new Outing{Id = outingDto.Id, Name = outingDto.Name}).ToList();
    }

    public bool Update(Outing outing)
    {
        return _outingRepository.Update(new OutingDto { Id = outing.Id, Name = outing.Name });
    }

    public bool Delete(int id)
    {
        return _outingRepository.Delete(id);
    }

    public List<Outing> GetAllFromTeam(int teamId)
    {
        return _outingRepository.GetAllFromTeam(teamId).Select(o => new Outing { Id = o.Id, Name = o.Name }).ToList();
    }
}