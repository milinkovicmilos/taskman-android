using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Client.Model;
using Client.Model.Response;
using Client.State;

namespace Client.Services;

public class AuthService
{
    private readonly HttpClientService _http;
    private JsonSerializerOptions _options;
    private AppState _appState;

    public AuthService(HttpClientService http, AppState appState)
    {
        _http = http;
        _options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        _appState = appState;
    }

    public async Task<MessageDataResponse<LoginResponseData?>> LoginAsync(LoginRequest request)
    {
        var json = JsonSerializer.Serialize(request, _options);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _http.PostAsync("/api/login", content);
        var jsonResponse = await response.Content.ReadAsStringAsync();

        var result = new MessageDataResponse<LoginResponseData?>();
        if (response.IsSuccessStatusCode)
        {
            var responseObj =
                JsonSerializer.Deserialize<MessageDataResponse<LoginResponseData>>(jsonResponse, _options);

            result.Message = responseObj.Message;
            result.Data = responseObj.Data;

            _appState.IsLoggedIn = true;
            _appState.UserFirstName = result.Data.FirstName;
            _appState.UserLastName = result.Data.LastName;
            _appState.UserEmail = result.Data.Email;
            await SecureStorage.SetAsync("token", result.Data.Token);
        }
        else
        {
            var errorObj =
                JsonSerializer.Deserialize<MessageResponse>(jsonResponse, _options);

            result.Message = errorObj.Message;
        }

        return result;
    }

    public async Task<MessageDataResponse<User>?> FetchUserAsync()
    {
        var response = await _http.GetAsync("/api/user");
        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<MessageDataResponse<User>>(json, _options);
    }
}