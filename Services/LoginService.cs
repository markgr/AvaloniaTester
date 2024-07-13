using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AvaloniaTester.Services
{
    internal interface ILoginService
    {
        Task<AuthenticationResult?> Authenticate(string username, string password);
        Task<DummyUser[]> Users();
    }

    internal class LoginService : ILoginService
    {
        private readonly HttpClient? _httpClient;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<AuthenticationResult?> Authenticate(string username, string password)
        {
            if (_httpClient is not null)
            {
                var response = await _httpClient.PostAsync("auth/login", JsonContent.Create(new
                {
                    username,
                    password
                }));

                var content = await response.Content.ReadAsStringAsync();
                return response.IsSuccessStatusCode ? JsonSerializer.Deserialize<AuthenticationResult>(content, JsonOptions) :
                    null;
            }
            return null;
        }

        public async Task<DummyUser[]> Users()
        {
            if (_httpClient is not null)
            {
                var response = await _httpClient.GetFromJsonAsync<UserResponse>("users");
                return response is null ? Array.Empty<DummyUser>() : response.Users;
            }

            return Array.Empty<DummyUser>();
        }
    }

    internal record AuthenticationResult(
        int Id,
        string Username,
        string Email,
        string FirstName,
        string LastName,
        string Gender,
        string Image,
        string Token,
        string RefreshToken);

    internal record UserResponse(DummyUser[] Users);

    internal record DummyUser(string Username, string FirstName, string LastName, string Password)
    {
        public string FullName => $"{FirstName} {LastName}";
    }

}
