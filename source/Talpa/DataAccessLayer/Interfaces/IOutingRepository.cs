using DataAccessLayer.Dtos;

namespace DataAccessLayer.Interfaces;

public interface IOutingRepository
{
    public OutingDto Create(OutingDto outingDto);

    public OutingDto? GetById(int id);
    
    public List<OutingDto> GetAll();
    
    public bool Update(OutingDto outingDto);

    public bool Delete(int id);
}