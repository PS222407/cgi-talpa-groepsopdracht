using DataAccessLayer.Dtos;

namespace DataAccessLayer.Interfaces;

public interface IOutingRepository
{
    public OutingDto Create(OutingDto outingDto, int teamId);

    public OutingDto? GetById(int id);
    
    public List<OutingDto> GetAll();
    
    public bool Update(OutingDto outingDto);

    public bool Delete(int id);
    
    List<OutingDto> GetAllFromTeam(int teamId);
}