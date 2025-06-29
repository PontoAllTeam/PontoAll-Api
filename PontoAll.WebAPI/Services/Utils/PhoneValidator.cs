using System.Text.RegularExpressions;

namespace PontoAll.WebAPI.Services.Utils;

public static class PhoneValidator
{
    public static void Validate(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Telefone é obrigatório.");

        if (!IsValidPhone(phone))
            throw new ArgumentException("Telefone inválido. Use DDD + número, ex: 11999999999.");
    }

    public static bool IsValidPhone(string phone)
    {
        return Regex.IsMatch(phone ?? "", @"^\d{10,11}$");
    }
}
