using System.Globalization;
using Newtonsoft.Json;

namespace GameStore.Application.Common.Converters;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string Format = "yyyy-MM-dd";

    public override void WriteJson(JsonWriter writer, DateOnly value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString(Format, DateTimeFormatInfo.InvariantInfo));
    }

    public override DateOnly ReadJson(JsonReader reader, Type objectType, DateOnly existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        var value = reader.Value?.ToString();

        if (value == null)
            return DateOnly.MinValue;

        return DateOnly.ParseExact(value, Format, DateTimeFormatInfo.InvariantInfo);
    }
}