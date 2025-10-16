using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Client.Model.Response;
using Client.Model.Tasks;

namespace Client.Services;

public class TasksService
{
    private readonly HttpClientService _http;
    private JsonSerializerOptions _options;

    public TasksService(HttpClientService httpClient)
    {
        _http = httpClient;
        _options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
    }

    public async Task<PaginatedResponse<TaskSummary>> FetchProjectsTasks(int projectId, int page = 1)
    {
        var response = await _http.GetAsync($"api/projects/{projectId}/tasks?page={page}");

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<PaginatedResponse<TaskSummary>>(jsonResponse, _options);
        if (result is null)
            throw new Exception("Failed to fetch projects tasks");

        return result;
    }

    public async Task<MessageDataResponse<CreateDataResponse?>> StoreTaskAsync(int projectId, CreateTaskRequest request)
    {
        var jsonOptions = new JsonSerializerOptions(_options)
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };
        var json = JsonSerializer.Serialize(request, jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _http.PostAsync($"api/projects/{projectId}/tasks", content);
        var jsonResponse = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<MessageDataResponse<CreateDataResponse?>>(jsonResponse, _options);
        if (result is null)
            throw new Exception("Failed to store task");

        return result;
    }
}