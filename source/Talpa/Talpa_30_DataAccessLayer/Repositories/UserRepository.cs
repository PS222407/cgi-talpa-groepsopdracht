using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using BusinessLogicLayer.Interfaces.Repositories;
using BusinessLogicLayer.Models;
using DataAccessLayer.Data;
using DataAccessLayer.Dtos;

namespace DataAccessLayer.Repositories;

public class UserRepository : Repository, IUserRepository
{
    private readonly DataContext _dataContext;

    private readonly string _baseUrl;

    public UserRepository(string clientId, string clientSecret, string domain, string apiClientId, string apiClientSecret, DataContext dataContext) : base(clientId, clientSecret, domain, apiClientId,
        apiClientSecret)
    {
        _dataContext = dataContext;
        _baseUrl = $"https://{domain}/api/v2";
    }

    public async Task<User?> GetById(string id)
    {
        using HttpClient httpClient = new();

        string url = $"{_baseUrl}{Auth0.GetUserEndpoint}/{id}";
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessToken());

        HttpResponseMessage response = await httpClient.GetAsync(url);
        string status = response.Content.ReadAsStringAsync().Result;
        UserDto? user = JsonSerializer.Deserialize<UserDto>(status);

        return user != null
            ? new User
            {
                Id = user.user_id,
                Email = user.email,
                Name = user.name,
                NickName = user.nickname,
                TeamId = user.user_metadata?.teamId,
            }
            : null;
    }

    public async Task<List<User>?> GetAll()
    {
        using HttpClient httpClient = new();

        string url = $"{_baseUrl}{Auth0.GetUserEndpoint}";
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessToken());

        HttpResponseMessage response = await httpClient.GetAsync(url);
        string jsonResponse = response.Content.ReadAsStringAsync().Result;
        List<UserDto>? userDtos = JsonSerializer.Deserialize<List<UserDto>>(jsonResponse);

        return userDtos?.Select(u => new User
        {
            Id = u.user_id,
            Email = u.email,
            Name = u.name,
            NickName = u.nickname,
            // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
            TeamId = u.user_metadata?.teamId,
        }).ToList();
    }

    public async Task<List<Role>?> GetRoles(string id)
    {
        using HttpClient httpClient = new();

        string url = $"{_baseUrl}{Auth0.GetUserEndpoint}/{id}/roles";
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessToken());

        HttpResponseMessage response = await httpClient.GetAsync(url);
        string status = response.Content.ReadAsStringAsync().Result;
        List<RoleDto>? roles = JsonSerializer.Deserialize<List<RoleDto>>(status);

        return roles?.Select(r => new Role { Id = r.id, Name = r.name, Description = r.description }).ToList();
    }

    public async Task<User?> GetByIdWithRoles(string id)
    {
        User? user = await GetById(id);
        if (user == null)
        {
            return null;
        }

        user.Roles = await GetRoles(id);

        return user;
    }

    public async Task<bool> UpdateTeam(string userId, int? teamId)
    {
        var patchData = new
        {
            user_metadata = new
            {
                teamId = teamId,
            },
        };
        string jsonData = JsonSerializer.Serialize(patchData);

        using HttpClient httpClient = new();

        string url = $"{_baseUrl}{Auth0.GetUserEndpoint}/{userId}";
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessToken());

        HttpRequestMessage request = new(HttpMethod.Patch, url)
        {
            Content = new StringContent(jsonData, Encoding.UTF8, "application/json"),
        };

        HttpResponseMessage response = await httpClient.SendAsync(request);

        return response.IsSuccessStatusCode;
    }

    public async Task<List<User>?> GetByTeam(int teamId)
    {
        using HttpClient httpClient = new();

        string searchQuery = $"user_metadata.teamId: {teamId}";
        string searchEngineVersion = "v3";
        string encodedSearchQuery = Uri.EscapeDataString(searchQuery);

        string url = $"{_baseUrl}{Auth0.GetUserEndpoint}?q={encodedSearchQuery}&search_engine={searchEngineVersion}";

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessToken());

        HttpResponseMessage response = await httpClient.GetAsync(url);
        string status = response.Content.ReadAsStringAsync().Result;
        List<UserDto>? users = JsonSerializer.Deserialize<List<UserDto>>(status);

        return users?.Select(u => new User
        {
            Id = u.user_id,
            Email = u.email,
            Name = u.name,
            NickName = u.nickname,
            TeamId = u.user_metadata.teamId,
        }).ToList();
    }

    public async Task<List<UserScoreboard>> GetTopTenUsersWhoOwnTheMostVotedSuggestions()
    {
        var query = _dataContext.SuggestionVote
            .GroupBy(sv => sv.SuggestionId)
            .Select(group => new
            {
                SuggestionId = group.Key,
                VoteCount = group.Count(),
            })
            .OrderByDescending(result => result.VoteCount)
            .Take(10)
            .Join(
                _dataContext.Suggestions,
                sv => sv.SuggestionId,
                s => s.Id,
                (sv, s) => new
                {
                    sv.VoteCount,
                    sv.SuggestionId,
                    s.UserId,
                    SuggestionName = s.Name,
                }
            );

        var results = query.ToList();
        
        List<UserScoreboard> userScoreboards = new();
        foreach (var result in results)
        {
            User? user = await GetById(result.UserId);
            userScoreboards.Add(new UserScoreboard()
            {
                UserId = result.UserId,
                UserName = user?.Name ?? "",
                SuggestionName = result.SuggestionName,
                SuggestionId = result.SuggestionId,
                VoteCount = result.VoteCount,
            });
        }
        
        return userScoreboards;
    }
}