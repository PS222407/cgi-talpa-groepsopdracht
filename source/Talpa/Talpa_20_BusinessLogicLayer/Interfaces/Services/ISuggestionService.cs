﻿using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces.Services;

public interface ISuggestionService
{
    public Suggestion Create(Suggestion suggestion, string userid);

    public Suggestion? GetById(int id);

    public List<Suggestion> GetByIds(List<int> ids);

    public List<Suggestion> GetAll();

    public List<Suggestion> GetAllByUserId(string id);

    public bool Update(Suggestion suggestion);

    public bool Delete(int id);
}