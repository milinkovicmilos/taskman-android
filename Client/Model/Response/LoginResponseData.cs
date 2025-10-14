using System.Text.Json.Serialization;

namespace Client.Model.Response;

public class LoginResponseData
{
    [JsonPropertyName("first_name")] public string FirstName { get; set; }
    [JsonPropertyName("last_name")] public string LastName { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}