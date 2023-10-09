using BusinessLogicLayer.Interfaces.Repositories;
using BusinessLogicLayer.Interfaces.Services;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Services;

public class RestrictionService : IRestrictionService
{
    private readonly IRestrictionRepository _restrictionRepository;

    public RestrictionService(IRestrictionRepository restrictionRepository)
    {
        _restrictionRepository = restrictionRepository;
    }

    public List<Restriction> GetAll()
    {
        return _restrictionRepository.GetAll();
    }
}