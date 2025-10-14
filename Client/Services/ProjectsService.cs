using System.Text.Json;
using Client.Model;
using Client.Model.Response;

namespace Client.Services;

public class ProjectsService
{
    private readonly HttpClientService _http;
    private JsonSerializerOptions _options;

    public ProjectsService(HttpClientService httpClient)
    {
        _http = httpClient;
        _options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
    }

    public async Task<PaginatedResponse<ProjectSummary>> FetchProjects(int page = 1)
    {
        var response = await _http.GetAsync($"api/projects?page={page}");

        var jsonResponse = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<PaginatedResponse<ProjectSummary>>(jsonResponse, _options);
        if (result is null)
            throw new Exception("Failed to get projects");

        return result;
    }
}