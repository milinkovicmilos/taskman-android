using System.Text.Json.Serialization;
using Client.Enums;
using Client.JsonConverters;

namespace Client.Model.Tasks;

public class TaskDetails
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public PriorityLevelOptions? Priority { get; set; }

    [JsonPropertyName("due_date")] public DateTime? DueDate { get; set; }

    [JsonConverter(typeof(BooleanConverter))]
    public bool Completed { get; set; }

    [JsonPropertyName("completed_at")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CompletedAt { get; set; }

    public string DateText
    {
        get
        {
            string str = string.Empty;

            if (DueDate.HasValue)
            {
                if (DueDate.Value.Date < DateTime.Now.Date)
                {
                    str += $"Task was due on {DueDate.Value.ToShortDateString()}.\n";
                }
                else
                {
                    str += $"Task is due on {DueDate.Value.ToShortDateString()}.\n";
                }
            }

            if (CompletedAt.HasValue)
            {
                if (DueDate.HasValue)
                {
                    if (DueDate.Value.Date < CompletedAt.Value.Date)
                    {
                        var diff = CompletedAt.Value.Date - DueDate.Value.Date;
                        str += $"Task was completed {diff.Days} late on {CompletedAt.Value.ToShortDateString()}.";
                    }
                    else if (DueDate.Value.Date > CompletedAt.Value.Date)
                    {
                        var diff = DueDate.Value.Date - CompletedAt.Value.Date;
                        str += $"Task was completed {diff.Days} early on {CompletedAt.Value.ToShortDateString()}.";
                    }
                    else
                    {
                        str += $"Task was completed on due date.";
                    }
                }
                else
                {
                    str += $"Task was completed on {CompletedAt.Value.ToShortDateString()}.";
                }
            }

            return str;
        }
    }

    public double PriorityOpacity
    {
        get
        {
            if (Priority.HasValue)
                return (int)Priority / 10.0;
            return 0;
        }
    }
}