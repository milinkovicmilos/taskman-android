using System.Text.Json;
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
}