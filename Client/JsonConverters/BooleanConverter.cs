using System.Text.Json;
using System.Text.Json.Serialization;

namespace Client.JsonConverters;

public class BooleanConverter : JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
        {
            if (reader.TryGetInt32(out int intValue))
            {
                if (intValue == 1)
                    return true;
                else if (intValue == 0)
                    return false;
            }
        }
        else if (reader.TokenType == JsonTokenType.True)
            return true;
        else if (reader.TokenType == JsonTokenType.False)
            return false;

        throw new JsonException($"Cannot convert {reader.TokenType} to bool");
    }

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    {
        if (value)
            writer.WriteNumberValue(1);
        writer.WriteNumberValue(0);
    }
}