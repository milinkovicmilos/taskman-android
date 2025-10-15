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

    [JsonConverter(typeof(DateTimeConverter))]
    [JsonPropertyName("completed_at")]
    public DateTime? CompletedAt { get; set; }

    public string DateText
    {
        get
        {
            if (DueDate.HasValue && CompletedAt.HasValue)
            {
                return $"Task was completed on {CompletedAt.Value.ToShortDateString()}";
            }
            else if (DueDate.HasValue && !CompletedAt.HasValue)
            {
                return $"Task is due on {DueDate.Value.ToShortDateString()}";
            }
            else if (CompletedAt.HasValue)
            {
                return $"Task was completed on {CompletedAt.Value.ToShortDateString()}";
            }

            return "";
        }
    }
}