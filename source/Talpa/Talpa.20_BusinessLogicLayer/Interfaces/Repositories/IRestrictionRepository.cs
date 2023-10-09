using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces.Repositories;

public interface IRestrictionRepository
{
    public List<Restriction> GetAll();
}