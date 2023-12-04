using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces.Repositories;

public interface IAppearanceRepository
{
    public Appearance? Get();
    
    public bool Update(Appearance appearance);
}