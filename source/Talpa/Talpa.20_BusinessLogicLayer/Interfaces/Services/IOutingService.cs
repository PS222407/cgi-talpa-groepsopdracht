using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces.Services;

public interface IOutingService
{
    public Outing Create(Outing outing, int teamId);

    public Outing? GetById(int id);
    
    public List<Outing> GetAll();
    
    public bool Update(Outing outing);

    public bool Delete(int id);
    
    public List<Outing> GetAllFromTeam(int teamId);
}