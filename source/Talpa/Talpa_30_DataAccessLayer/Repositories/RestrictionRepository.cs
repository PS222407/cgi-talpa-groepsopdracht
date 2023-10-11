using BusinessLogicLayer.Interfaces.Repositories;
using BusinessLogicLayer.Models;
using DataAccessLayer.Data;

namespace DataAccessLayer.Repositories;

public class RestrictionRepository : IRestrictionRepository
{
    private readonly DataContext _dataContext;

    public RestrictionRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public List<Restriction> GetAll()
    {
        return _dataContext.Restrictions.ToList();
    }
}