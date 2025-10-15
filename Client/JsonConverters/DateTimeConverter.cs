using System.Text.Json;
using System.Text.Json.Serialization;

namespace Client.JsonConverters;

public class DateTimeConverter : JsonConverter<DateTime>
{
    private string format = "yyyy-MM-dd HH:mm:ss";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (value is null)
            throw new JsonException("Cannot convert null string to DateTime");

        return DateTime.ParseExact(value, format, null);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(format));
    }
}