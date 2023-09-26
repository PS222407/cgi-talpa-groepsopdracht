using DataAccessLayer.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    public DbSet<OutingDto> Outings { get; set; }

    public DbSet<TeamDto> Teams { get; set; }
}