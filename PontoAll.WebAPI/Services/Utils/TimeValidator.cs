using PontoAll.WebAPI.Objects.Dtos.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace PontoAll.WebAPI.Services.Utils;

public static class TimeValidator
{
    public static void ValidateTime(object value, string fieldName = "Horário")
    {
        if (value is string strValue)
        {
            if (!TimeOnly.TryParseExact(strValue, "HH:mm:ss", out _))
                throw new Exception($"{fieldName} inválido. Use o formato HH:mm:ss.");
        }
        else if (value is TimeOnly)
        {
            
        }
        else
        {
            throw new Exception($"{fieldName} está em um formato inválido.");
        }
    }
}


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

