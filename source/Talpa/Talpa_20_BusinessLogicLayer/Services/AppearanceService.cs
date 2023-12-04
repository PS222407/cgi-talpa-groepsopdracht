using BusinessLogicLayer.Interfaces.Repositories;
using BusinessLogicLayer.Interfaces.Services;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Services;

public class AppearanceService : IAppearanceService
{
    private readonly IAppearanceRepository _appearanceRepository;

    public AppearanceService(IAppearanceRepository appearanceRepository)
    {
        _appearanceRepository = appearanceRepository;
    }
    
    public Appearance? Get()
    {
        return _appearanceRepository.Get();
    }

    public bool Update(Appearance appearance)
    {
        return _appearanceRepository.Update(appearance);
    }
}