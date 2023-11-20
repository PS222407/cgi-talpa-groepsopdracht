﻿using BusinessLogicLayer.Interfaces.Repositories;
using BusinessLogicLayer.Interfaces.Services;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Services;

public class OutingService : IOutingService
{
    private readonly IOutingRepository _outingRepository;

    public OutingService(IOutingRepository outingRepository)
    {
        _outingRepository = outingRepository;
    }

    public Outing Create(Outing outing, int teamId)
    {
        return _outingRepository.Create(outing, teamId);
    }

    public Outing? GetById(int id)
    {
        return _outingRepository.GetById(id);
    }

    public Outing? GetByIdWithVotes(int id)
    {
        return _outingRepository.GetByIdWithVotes(id);
    }

    public List<Outing> GetAll()
    {
        return _outingRepository.GetAll();
    }

    public List<Outing> GetAllComplete()
    {
        List<Outing> completeOutings = _outingRepository.GetAllComplete();

        return completeOutings.Where(completeOuting => completeOuting.DeadLine > DateTime.Now).ToList();
    }

    public bool Update(Outing outing)
    {
        if (outing.Suggestions?.Count > 3)
        {
            return false;
        }

        return _outingRepository.Update(outing);
    }

    public bool Delete(int id)
    {
        return _outingRepository.Delete(id);
    }

    public List<Outing> GetAllFromTeam(int teamId)
    {
        return _outingRepository.GetAllFromTeam(teamId);
    }

    public List<Outing> GetAllCompleteFromTeam(int teamId)
    {
        List<Outing> completeOutings = _outingRepository.GetAllCompleteFromTeam(teamId);

        return completeOutings.Where(completeOuting => completeOuting.DeadLine > DateTime.Now).ToList();
    }

    public bool UserHasVotedForOuting(string userId, int outingId)
    {
        return _outingRepository.UserHasVotedDatesForOuting(userId, outingId)
               || _outingRepository.UserHasVotedSuggestionForOuting(userId, outingId);
    }

    public bool Vote(string userId, int outingId, int suggestionId, List<int> votedDateIds)
    {
        return !UserHasVotedForOuting(userId, outingId)
               && _outingRepository.Vote(userId, outingId, suggestionId, votedDateIds);
    }

    public Outing? GetOutingByIdWithMostVotedDatesAndSuggestions(int id)
    {
        return _outingRepository.GetOutingByIdWithMostVotedDatesAndSuggestions(id);
    }

    public bool ConfirmOuting(int id, int suggestionId, int outingDateId)
    {
        return _outingRepository.ConfirmOuting(id, suggestionId, outingDateId);
    }

    public List<Outing> GetAllConfirmedFromTeam(int teamId)
    {
        return _outingRepository.GetAllConfirmedFromTeam(teamId);
    }
}