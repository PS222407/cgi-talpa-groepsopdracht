using BusinessLogicLayer.Interfaces.Repositories;
using BusinessLogicLayer.Models;
using DataAccessLayer.Data;

namespace DataAccessLayer.Repositories;

public class AppearanceRepository : IAppearanceRepository
{
    private readonly DataContext _dataContext;

    public AppearanceRepository(DataContext dbContext)
    {
        _dataContext = dbContext;
    }

    public Appearance? Get()
    {
        return _dataContext.Appearances.FirstOrDefault();
    }

    public bool Update(Appearance appearance)
    {
        List<Appearance> appearances = _dataContext.Appearances.ToList();
        foreach (Appearance appearanceDb in appearances)
        {
            _dataContext.Remove(appearanceDb);
        }
        
        _dataContext.Appearances.Update(appearance);
        _dataContext.SaveChanges();

        return true;
    }
}