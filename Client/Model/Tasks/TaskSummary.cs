using System.Text.Json.Serialization;
using Client.JsonConverters;

namespace Client.Model.Tasks;

public class TaskSummary
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? Priority { get; set; }
    [JsonPropertyName("due_date")] public DateTime? DueDate { get; set; }

    [JsonConverter(typeof(BooleanConverter))]
    public bool Completed { get; set; }

    [JsonPropertyName("completed_at")] public DateTime? CompletedAt { get; set; }
}