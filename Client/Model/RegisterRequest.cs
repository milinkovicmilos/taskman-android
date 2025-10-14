using System.Text.Json.Serialization;

namespace Client.Model;

public class RegisterRequest
{
    [JsonPropertyName("first_name")] public string FirstName { get; set; }
    [JsonPropertyName("last_name")] public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    [JsonPropertyName("password_confirmation")]
    public string PasswordConfirmation { get; set; }
}