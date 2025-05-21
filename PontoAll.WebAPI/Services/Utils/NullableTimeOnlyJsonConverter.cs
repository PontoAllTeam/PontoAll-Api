using System.Text.Json;
using System.Text.Json.Serialization;

namespace PontoAll.WebAPI.Objects.Utils;
public class NullableTimeOnlyJsonConverter : JsonConverter<TimeOnly?>
{
    public override TimeOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;

        if (reader.TokenType == JsonTokenType.String)
        {
            var str = reader.GetString();
            if (string.IsNullOrWhiteSpace(str))
                return null;

            if (TimeOnly.TryParse(str, out var result))
            {
                // Validação adicional: deve estar entre 00:00:00 e 23:59:59
                if (result > new TimeOnly(23, 59, 59))
                    throw new JsonException($"Horário inválido: {str}. O horário deve estar entre 00:00:00 e 23:59:59.");

                return result;
            }

            // Se o formato estiver incorreto
            throw new JsonException($"Formato de horário inválido: {str}. Use o formato HH:mm:ss (ex: 08:30:00).");
        }

        // Caso o tipo do token não seja uma string ou null
        throw new JsonException($"Tipo inesperado para TimeOnly: {reader.TokenType}. Esperado um valor de horário em string.");
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteStringValue(value.Value.ToString("HH:mm:ss"));
        else
            writer.WriteNullValue();
    }
}
