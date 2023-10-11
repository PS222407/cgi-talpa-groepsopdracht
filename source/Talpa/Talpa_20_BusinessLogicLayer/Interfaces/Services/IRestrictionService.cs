using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces.Services;

public interface IRestrictionService
{
    public List<Restriction> GetAll();
}