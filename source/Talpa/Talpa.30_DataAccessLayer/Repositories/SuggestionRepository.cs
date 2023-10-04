﻿using BusinessLogicLayer.Interfaces.Repositories;
using BusinessLogicLayer.Models;
using DataAccessLayer.Data;

namespace DataAccessLayer.Repositories;

public class SuggestionRepository : ISuggestionRepository
{
    private readonly DataContext _dataContext;

    public SuggestionRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public List<Suggestion> GetAll()
    {
        return _dataContext.Suggestions.ToList();
    }
}