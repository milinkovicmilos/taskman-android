using System.Text.Json.Serialization;

namespace Client.Model.Response;

public class PaginatedResponse<T>
{
    [JsonPropertyName("per_page")] public int PerPage { get; set; }
    [JsonPropertyName("current_page")] public int CurrentPage { get; set; }
    [JsonPropertyName("last_page")] public int LastPage { get; set; }
    public ICollection<T> Data { get; set; } = [];
}