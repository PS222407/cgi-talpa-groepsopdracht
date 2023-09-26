using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces;

public interface IOutingService
{
    public Outing Create(Outing outing);

    public Outing? GetById(int id);
    
    public List<Outing> GetAll();
    
    public bool Update(Outing outing);

    public bool Delete(int id);
}