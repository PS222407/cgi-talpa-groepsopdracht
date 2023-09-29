using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces.Repositories;

public interface IOutingRepository
{
    public Outing Create(Outing outing, int teamId);

    public Outing? GetById(int id);
    
    public List<Outing> GetAll();
    
    public bool Update(Outing outing);

    public bool Delete(int id);
    
    List<Outing> GetAllFromTeam(int teamId);
}