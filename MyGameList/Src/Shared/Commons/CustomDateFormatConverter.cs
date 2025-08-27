using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

public class CustomDateFormatConverter : JsonConverter<DateTime?>
{
    private const string DateFormat = "yyyy-MM-dd";

    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            if (DateTime.TryParseExact(reader.GetString(), DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                return date;
            }
        }
        return null;
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            writer.WriteStringValue(value.Value.ToString(DateFormat));
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}