using System.Net.Http.Headers;
using System.Text.Json;
using DataAccessLayer.Dtos;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories;

public class UserRepository : Repository, IUserRepository
{
    public UserRepository(string clientId, string clientSecret) : base(clientId, clientSecret)
    {
    }

    public async Task<UserDto?> GetUserById(string id)
    {
        using HttpClient httpClient = new HttpClient();
        
        string url = $"{Auth0.BaseUrl}{Auth0.GetUserEndpoint}/{id}";
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAccessToken());
        
        HttpResponseMessage response = await httpClient.GetAsync(url);
        string status = response.Content.ReadAsStringAsync().Result;
        UserDto? userDto = JsonSerializer.Deserialize<UserDto>(status);

        return userDto;
    }
}
