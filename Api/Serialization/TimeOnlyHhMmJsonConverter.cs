using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Api.Serialization;

/// <summary>
/// Serializa/deserializa <see cref="TimeOnly"/> estrictamente como HH:mm
/// (sin segundos ni fracciones).
/// </summary>
public sealed class TimeOnlyHhMmJsonConverter : JsonConverter<TimeOnly>
{
    private const string Format = "HH:mm";

    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
            throw new JsonException("Las horas deben enviarse como texto HH:mm (por ejemplo \"08:30\"), no como número.");

        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException("Las horas deben enviarse como texto en formato HH:mm.");

        var text = reader.GetString();
        if (string.IsNullOrWhiteSpace(text))
            throw new JsonException("La hora no puede estar vacía. Use formato HH:mm.");

        if (!TimeOnly.TryParseExact(text, Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var time))
            throw new JsonException($"Formato de hora inválido: '{text}'. Use HH:mm (sin segundos).");

        return time;
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
}
