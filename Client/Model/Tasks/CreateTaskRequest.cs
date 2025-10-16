using System.Text.Json.Serialization;
using Client.Enums;

namespace Client.Model.Tasks;

public class CreateTaskRequest
{
    [JsonPropertyName("project_id")] public int ProjectId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    [JsonPropertyName("due_date")] public DateTime? DueDate { get; set; }
    public PriorityLevelOptions? Priority { get; set; }
}