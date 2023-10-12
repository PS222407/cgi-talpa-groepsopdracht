using BusinessLogicLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Outing> Outings { get; set; }

    public DbSet<Team> Teams { get; set; }

    public DbSet<Suggestion> Suggestions { get; set; }

    public DbSet<Restriction> Restrictions { get; set; }

    public DbSet<SuggestionDate> SuggestionDates { get; set; }

    public DbSet<SuggestionVote> SuggestionVote { get; set; }

    public DbSet<OutingDate> OutingDates { get; set; }

    public DbSet<DateVote> DateVotes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<SuggestionVote>().HasKey(s => new { s.SuggestionId, s.OutingId, s.UserId });
        builder.Entity<DateVote>().HasKey(d => new { d.UserId, d.OutingDateId });
    }
}