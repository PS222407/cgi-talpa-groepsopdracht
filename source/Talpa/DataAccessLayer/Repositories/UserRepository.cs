using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using DataAccessLayer.Dtos;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories;

public class UserRepository : Repository, IUserRepository
{
    public UserRepository(string clientId, string clientSecret) : base(clientId, clientSecret)
    {
    }

    public async Task<UserDto?> GetById(string id)
    {
        using HttpClient httpClient = new HttpClient();
        
        string url = $"{Auth0.BaseUrl}{Auth0.GetUserEndpoint}/{id}";
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessToken());
        
        HttpResponseMessage response = await httpClient.GetAsync(url);
        string status = response.Content.ReadAsStringAsync().Result;
        UserDto? userDto = JsonSerializer.Deserialize<UserDto>(status);

        return userDto;
    }

    public async Task<List<UserDto>?> GetAll()
    {
        using HttpClient httpClient = new HttpClient();
        
        string url = $"{Auth0.BaseUrl}{Auth0.GetUserEndpoint}";
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessToken());
        
        HttpResponseMessage response = await httpClient.GetAsync(url);
        string status = response.Content.ReadAsStringAsync().Result;
        List<UserDto>? userDtos = JsonSerializer.Deserialize<List<UserDto>>(status);

        return userDtos;
    }

    public async Task<List<RoleDto>?> GetRoles(string id)
    {
        using HttpClient httpClient = new HttpClient();
        
        string url = $"{Auth0.BaseUrl}{Auth0.GetUserEndpoint}/{id}/roles";
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessToken());
        
        HttpResponseMessage response = await httpClient.GetAsync(url);
        string status = response.Content.ReadAsStringAsync().Result;
        List<RoleDto>? roleDtos = JsonSerializer.Deserialize<List<RoleDto>>(status);

        return roleDtos;
    }

    public async Task<UserDto?> GetByIdWithRoles(string id)
    {
        UserDto? userDto = await GetById(id);
        if (userDto == null)
        {
            return null;
        }

        userDto.roles = await GetRoles(id);
        
        return userDto;
    }

    public async Task<bool> UpdateTeam(string userId, int? teamId)
    {
        var patchData = new
        {
            user_metadata = new
            {
                teamId = teamId
            }
        };
        string jsonData = JsonSerializer.Serialize(patchData);
        
        using HttpClient httpClient = new HttpClient();
        
        string url = $"{Auth0.BaseUrl}{Auth0.GetUserEndpoint}/{userId}";
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessToken());
        
        var request = new HttpRequestMessage(HttpMethod.Patch, url)
        {
            Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
        };

        HttpResponseMessage response = await httpClient.SendAsync(request);

        return response.IsSuccessStatusCode;
    }

    public async Task<List<UserDto>?> GetByTeam(int teamId)
    {
        using HttpClient httpClient = new HttpClient();
        
        string searchQuery = $"user_metadata.teamId: {teamId}";
        string searchEngineVersion = "v3";
        string encodedSearchQuery = Uri.EscapeDataString(searchQuery);

        string url = $"{Auth0.BaseUrl}{Auth0.GetUserEndpoint}?q={encodedSearchQuery}&search_engine={searchEngineVersion}";
        
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessToken());
        
        HttpResponseMessage response = await httpClient.GetAsync(url);
        string status = response.Content.ReadAsStringAsync().Result;
        List<UserDto>? userDtos = JsonSerializer.Deserialize<List<UserDto>>(status);
        
        return userDtos;
    }
}
