using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces.Services;

public interface IAppearanceService
{
    public Appearance? Get();
    
    public bool Update(Appearance appearance);
}