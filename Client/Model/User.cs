using System.Text.Json.Serialization;

namespace Client.Model;

public class User
{
    [JsonPropertyName("first_name")] public string FirstName { get; set; }
    [JsonPropertyName("last_name")] public string LastName { get; set; }
    public string Email { get; set; }
}