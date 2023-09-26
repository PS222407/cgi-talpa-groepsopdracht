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

    public Outing Create(Outing outing)
    {
        OutingDto outingDto = _outingRepository.Create(new OutingDto(null, outing.Name));
        
        return new Outing(outingDto.Id, outingDto.Name);
    }

    public Outing? GetById(int id)
    {
        OutingDto? outingDto = _outingRepository.GetById(id);
        if (outingDto == null)
        {
            return null;
        }
        
        return new Outing(outingDto.Id, outingDto.Name);
    }

    public List<Outing> GetAll()
    {
        return _outingRepository.GetAll().Select(outingDto => new Outing(outingDto.Id, outingDto.Name)).ToList();
    }

    public bool Update(Outing outing)
    {
        return _outingRepository.Update(new OutingDto(outing.Id, outing.Name));
    }

    public bool Delete(int id)
    {
        return _outingRepository.Delete(id);
    }
}