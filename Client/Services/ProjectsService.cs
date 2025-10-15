using System.Text;
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

    public async Task<MessageDataResponse<CreateDataResponse?>> StoreProject(CreateProjectRequest request)
    {
        var json = JsonSerializer.Serialize(request, _options);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _http.PostAsync("api/projects", content);
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<MessageDataResponse<CreateDataResponse?>>(jsonResponse, _options);

        if (result is null)
            throw new Exception("Failed to store project");
        return result;
    }

    public async Task<ProjectDetails> FetchProjectDetails(int id)
    {
        var response = await _http.GetAsync($"api/projects/{id}");

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<ProjectDetails>(jsonResponse, _options);
        if (result is null)
            throw new Exception("Failed to get project details");

        return result;
    }
}