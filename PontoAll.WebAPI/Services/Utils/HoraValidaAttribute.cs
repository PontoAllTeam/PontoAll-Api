using System.ComponentModel.DataAnnotations;

namespace PontoAll.WebAPI.Objects.Utils;

public class HoraValidaAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var horaStr = value as string;

        // Permite campos vazios ou nulos
        if (string.IsNullOrWhiteSpace(horaStr))
            return ValidationResult.Success;

        if (!TimeOnly.TryParseExact(horaStr, "HH:mm:ss", out var _))
        {
            return new ValidationResult(
                $"O valor '{horaStr}' em {validationContext.DisplayName} não é um horário válido. Use o formato HH:mm:ss entre 00:00:00 e 23:59:59."
            );
        }

        return ValidationResult.Success;
    }
}
