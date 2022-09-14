using System.Text.Json.Serialization;
using System.Text.Json;

namespace AIHR.Test.Converters;

internal sealed class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => DateOnly.Parse(reader.GetString()!);

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString("O"));
}