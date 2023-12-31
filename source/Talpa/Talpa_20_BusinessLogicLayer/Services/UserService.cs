﻿using BusinessLogicLayer.Interfaces.Repositories;
using BusinessLogicLayer.Interfaces.Services;
using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetById(string id)
    {
        return await _userRepository.GetById(id);
    }

    public async Task<List<User>?> GetAll()
    {
        return await _userRepository.GetAll();
    }

    public async Task<List<Role>?> GetRoles(string id)
    {
        return await _userRepository.GetRoles(id);
    }

    public async Task<User?> GetByIdWithRoles(string id)
    {
        return await _userRepository.GetByIdWithRoles(id);
    }

    public async Task<bool> UpdateTeam(string id, int teamId)
    {
        return await _userRepository.UpdateTeam(id, teamId);
    }

    public async Task<List<User>?> GetByTeam(int teamId)
    {
        return await _userRepository.GetByTeam(teamId);
    }

    public async Task<List<UserScoreboard>> GetTopTenUsersWhoOwnTheMostVotedSuggestions()
    {
        List<UserScoreboard> userScoreboards = await _userRepository.GetTopTenUsersWhoOwnTheMostVotedSuggestions();
        if (!userScoreboards.Any())
        {
            return userScoreboards;
        }
        
        int max = userScoreboards.Max(userScoreboard => userScoreboard.VoteCount);
        foreach (UserScoreboard userScoreboard in userScoreboards)
        {
            userScoreboard.VotePercentage = (int)Math.Round((double)userScoreboard.VoteCount / max * 100);
        }

        return userScoreboards;
    }
}